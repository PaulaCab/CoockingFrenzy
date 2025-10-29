using UnityEngine;

public interface IKitchenObjParent
{
    public Transform GetObjPoint();
    public KitchenObj GetKitchenObj();
    public void SetKitchenObj(KitchenObj kitchenObj);
    public void ClearKitchenObj();
    public bool HasKitchenObj();
}
