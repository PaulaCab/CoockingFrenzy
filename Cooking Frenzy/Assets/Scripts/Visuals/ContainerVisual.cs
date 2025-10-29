using System;
using UnityEngine;

public class ContainerVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    
    [SerializeField] private ContainerCounter counter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (counter != null)
            counter.OnObjGrabbed += Counter_OnObjGrabbed;
    }

    private void Counter_OnObjGrabbed(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
