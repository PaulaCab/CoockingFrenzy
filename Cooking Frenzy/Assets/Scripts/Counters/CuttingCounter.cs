using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;
    public override void Interact(Player player)
    {
        if (!HasKitchenObj())
        {
            if(player.HasKitchenObj() && HasRecipe(player.GetKitchenObj().GetKitchenObjSO()))
                player.GetKitchenObj().SetKitchenObjParent(this);
        }
        else
        {
            if(!player.HasKitchenObj())
                kitchenObj.SetKitchenObjParent(player);
        }
    }
    
    public override void InteractAlt(Player player)
    {
        if (!HasKitchenObj())
            return;
        
        KitchenObjSO outputSO = GetRecipeOutput(kitchenObj.GetKitchenObjSO());
        if (!outputSO)
            return;
            
        kitchenObj.DestroySelf();
        KitchenObj.SpawnKitchenObj(outputSO, this);
        
    }

    private bool HasRecipe(KitchenObjSO inputSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == inputSO)
                return true;
        }
        return false;
    }

    private KitchenObjSO GetRecipeOutput(KitchenObjSO inputSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == inputSO)
                return recipe.output;
        }

        return null;
    }
}
