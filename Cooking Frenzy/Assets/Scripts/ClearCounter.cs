using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjSO kitchenObjSO;
    [SerializeField] private Transform topPoint;

    private KitchenObj kitchenObj;
    public void Interact()
    {
        if (kitchenObj == null)
        {
            GameObject obj = Instantiate(kitchenObjSO.prefab, topPoint);
            obj.transform.localPosition = Vector3.zero;

            kitchenObj = obj.GetComponent<KitchenObj>();
        }

    }
}
