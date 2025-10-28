using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter counter;
    [SerializeField] private GameObject visual;
    private void Start()
    {
        Player.Instance.OnSelectedChanged += Player_OnSelectedChanged;
    }

    private void Player_OnSelectedChanged(object sender, Player.OnSelectedChangedEventArgs e)
    {
        if(e.selected == counter)
            visual.SetActive(true);
        else
            visual.SetActive(false);
    }
}
