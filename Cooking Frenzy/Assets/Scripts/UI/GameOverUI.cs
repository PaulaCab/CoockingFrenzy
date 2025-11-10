using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipeNumText;
    
    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        gameObject.SetActive(false);
    }
    
    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOverActive())
        {
            gameObject.SetActive(true);
            RecipeNumText.text = DeliveryManager.Instance.GetDeliveredRecipes().ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
