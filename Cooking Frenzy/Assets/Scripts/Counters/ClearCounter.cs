using UnityEditor;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObj())
        {
            if(player.HasKitchenObj())
                player.GetKitchenObj().SetKitchenObjParent(this);
        }
        else
        {
            if (player.HasKitchenObj())
            {
                if (player.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateObj))
                {
                    if(plateObj.TryAddIngredient(kitchenObj.GetKitchenObjSO()))
                        kitchenObj.DestroySelf();
                }
                else if (kitchenObj.TryGetPlate(out plateObj))
                {
                    if(plateObj.TryAddIngredient(player.GetKitchenObj().GetKitchenObjSO()))
                        player.GetKitchenObj().DestroySelf();
                        
                }
            }
            else
                kitchenObj.SetKitchenObjParent(player);
        }
    }

}
