using System;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownTimer()).ToString();
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownActive())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
