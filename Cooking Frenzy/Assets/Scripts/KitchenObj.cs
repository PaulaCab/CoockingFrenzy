using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    [SerializeField] private KitchenObjSO kitchenObjSO;
    public KitchenObjSO GetKitchenObjSo() {return kitchenObjSO;}

    private IKitchenObjParent kitchenObjParent;

    public void SetKitchenObjParent(IKitchenObjParent kitchenObjParent)
    {
        if(this.kitchenObjParent != null)
            this.kitchenObjParent.ClearKitchenObj();
        
        this.kitchenObjParent = kitchenObjParent;
        kitchenObjParent.SetKitchenObj(this);

        transform.parent = kitchenObjParent.GetObjPoint();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjParent GetKitchenObjParent(){return kitchenObjParent;}
}
