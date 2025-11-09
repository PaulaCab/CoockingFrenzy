using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObject(KitchenObjSO kitchenObjSO)
    {
        image.sprite = kitchenObjSO.sprite;
    }
}
