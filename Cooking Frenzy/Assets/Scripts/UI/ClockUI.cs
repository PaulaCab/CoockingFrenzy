using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    public void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
    }
}
