using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjParent
{
    public static event EventHandler OnDrop;
    
    [SerializeField] protected Transform topPoint;
    protected KitchenObj kitchenObj;
    public virtual void Interact(Player player) { }
    public virtual void InteractAlt(Player player) { }
    
    //IKitchenObjParent Implementation
    public Transform GetObjPoint() { return topPoint; }
    public KitchenObj GetKitchenObj() {return kitchenObj;}

    public void SetKitchenObj(KitchenObj kitchenObj)
    {
        this.kitchenObj = kitchenObj;
        
        if(kitchenObj)
            OnDrop?.Invoke(this, EventArgs.Empty);
    }
    public void ClearKitchenObj() { kitchenObj = null;}
    public bool HasKitchenObj() { return kitchenObj != null;}
}
