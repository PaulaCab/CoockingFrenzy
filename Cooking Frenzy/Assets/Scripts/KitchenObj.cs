using UnityEngine;

public class KitchenObj : MonoBehaviour
{
    [SerializeField] private KitchenObjSO kitchenObjSO;
    public KitchenObjSO GetKitchenObjSo() {return kitchenObjSO;}

    private ClearCounter clearCounter;
    public void SetClearCounter(ClearCounter clearCounter) { this.clearCounter = clearCounter; }
    public ClearCounter GetClearCounter(){return clearCounter;}
}
