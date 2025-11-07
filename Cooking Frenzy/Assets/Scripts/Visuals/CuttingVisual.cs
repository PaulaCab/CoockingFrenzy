using System;
using UnityEngine;

public class CuttingVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    
    [SerializeField] private CuttingCounter counter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (counter != null)
            counter.OnProgressChanged += Counter_OnProgressChanged;
    }

    private void Counter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if(e.progressNormalized>0)
            animator.SetTrigger(CUT);
    }
}
