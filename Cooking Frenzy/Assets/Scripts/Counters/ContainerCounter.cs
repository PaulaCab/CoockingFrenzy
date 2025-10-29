using System;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjParent
{
    public event EventHandler OnObjGrabbed;
    
    [SerializeField] private KitchenObjSO kitchenObjSO;
    [SerializeField] private SpriteRenderer objSprite;
    
    private void OnValidate()
    {
        if (kitchenObjSO != null && objSprite != null)
            objSprite.sprite = kitchenObjSO.sprite;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObj())
            return;
        
        GameObject obj = Instantiate(kitchenObjSO.prefab);
        obj.GetComponent<KitchenObj>().SetKitchenObjParent(player);
        OnObjGrabbed?.Invoke(this, EventArgs.Empty);
    }
}
