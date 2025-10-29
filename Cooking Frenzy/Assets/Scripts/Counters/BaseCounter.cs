using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjParent
{
    [SerializeField] protected Transform topPoint;
    protected KitchenObj kitchenObj;
    public virtual void Interact(Player player)
    {
    }
    
    //IKitchenObjParent Implementation
    public Transform GetObjPoint() { return topPoint; }
    public KitchenObj GetKitchenObj() {return kitchenObj;}
    public void SetKitchenObj(KitchenObj kitchenObj) { this.kitchenObj = kitchenObj;}
    public void ClearKitchenObj() { kitchenObj = null;}
    public bool HasKitchenObj() { return kitchenObj != null;}
}
