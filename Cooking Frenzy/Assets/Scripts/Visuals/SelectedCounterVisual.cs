using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject[] visuals;
    private void Start()
    {
        Player.Instance.OnSelectedChanged += Player_OnSelectedChanged;
    }

    private void Player_OnSelectedChanged(object sender, Player.OnSelectedChangedEventArgs e)
    {
        foreach (GameObject obj in visuals)
        {
            if(e.selected == counter)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }
    }
}
