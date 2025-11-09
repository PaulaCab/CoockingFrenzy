using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGO;
    [SerializeField] private Image barImg;
    private IHasProgress hasProgress;
    
    private void Start()
    {
        hasProgress = hasProgressGO.GetComponent<IHasProgress>();
        if (hasProgress!=null)
            hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImg.fillAmount = 0;
        gameObject.SetActive(false);
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImg.fillAmount = e.progressNormalized;
        
        if(e.progressNormalized==0f || e.progressNormalized==1f)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    
}
