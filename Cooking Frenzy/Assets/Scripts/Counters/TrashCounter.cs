using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnThrowAway;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObj())
        {
            player.GetKitchenObj().DestroySelf();
            OnThrowAway?.Invoke(this, EventArgs.Empty);
        }
    }
}
