using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChange;
    private enum State
    {
        Waiting,
        Countdown,
        Playing,
        GameOver,
    }

    private State state;
    private float waitingTimer = 1f;
    private float countdownTimer = 3f;
    private float playingTimer = 0f;
    [SerializeField] private float playingTimerMax = 30f;

    private void Awake()
    {
        Instance = this;
        state = State.Waiting;
        playingTimer = playingTimerMax;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Waiting:
                waitingTimer -= Time.deltaTime;
                if (waitingTimer < 0)
                {
                    state = State.Countdown;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.Countdown:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer < 0)
                {
                    state = State.Playing;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.Playing:
                playingTimer -= Time.deltaTime;
                if (playingTimer < 0)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.Playing;
    }
    
    public bool IsCountdownActive()
    {
        return state == State.Countdown;
    }
    
    public bool IsGameOverActive()
    {
        return state == State.GameOver;
    }

    public float GetCountdownTimer()
    {
        return countdownTimer;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (playingTimer / playingTimerMax);
    }
}
