using System;
using UnityEngine;

public class StoveVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter counter;
    [SerializeField] private GameObject stove;
    [SerializeField] private GameObject particles;

    private void Start()
    {
        counter.OnStateChanged += Counter_OnStateChanged;
    }

    private void Counter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool show = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        stove.SetActive(show);
        particles.SetActive(show);
    }
}
