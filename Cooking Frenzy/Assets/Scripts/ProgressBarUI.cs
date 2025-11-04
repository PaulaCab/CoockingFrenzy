using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter counter;
    [SerializeField] private Image barImg;

    private void Start()
    {
        if (counter)
            counter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        barImg.fillAmount = 0;
        gameObject.SetActive(false);
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImg.fillAmount = e.progressNormalized;
        
        if(e.progressNormalized==0f || e.progressNormalized==1f)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    
}
