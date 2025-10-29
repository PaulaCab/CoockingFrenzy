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
            if(!player.HasKitchenObj())
                kitchenObj.SetKitchenObjParent(player);
        }
    }

}
