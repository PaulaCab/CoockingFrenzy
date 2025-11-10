using System;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter counter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        counter.OnStateChanged += Counter_OnStateChanged;
    }

    private void Counter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
