using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObj())
            return;

        if (player.GetKitchenObj().TryGetPlate(out PlateKitchenObject plate))
        {
            DeliveryManager.Instance.DeliverRecipe(plate);
            plate.DestroySelf();
        }
    }
}
