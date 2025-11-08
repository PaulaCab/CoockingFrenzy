using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    [SerializeField] private KitchenObjSO kitchenObjSO;
    public KitchenObjSO GetKitchenObjSO() {return kitchenObjSO;}

    private IKitchenObjParent kitchenObjParent;
    public IKitchenObjParent GetKitchenObjParent(){return kitchenObjParent;}
    public void SetKitchenObjParent(IKitchenObjParent kitchenObjParent)
    {
        if(this.kitchenObjParent != null)
            this.kitchenObjParent.ClearKitchenObj();
        
        this.kitchenObjParent = kitchenObjParent;
        kitchenObjParent.SetKitchenObj(this);

        transform.parent = kitchenObjParent.GetObjPoint();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        kitchenObjParent.ClearKitchenObj();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }

    public static KitchenObj SpawnKitchenObj(KitchenObjSO objSO, IKitchenObjParent parent)
    {
        GameObject kitchenObjGO = Instantiate(objSO.prefab);
        KitchenObj kitchenObj = kitchenObjGO.GetComponent<KitchenObj>();
        kitchenObj.SetKitchenObjParent(parent);
        return kitchenObj;
    }
}
