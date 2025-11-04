using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }


    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    private int cuttingProgress = 0;
    public override void Interact(Player player)
    {
        if (!HasKitchenObj())
        {
            if (player.HasKitchenObj() && HasRecipe(player.GetKitchenObj().GetKitchenObjSO()))
            {
                player.GetKitchenObj().SetKitchenObjParent(this);
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs{progressNormalized = 0});
            }
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

        CuttingRecipeSO recipe = GetRecipe(kitchenObj.GetKitchenObjSO());
        if (!recipe)
            return;
        
        cuttingProgress++;
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress/recipe.cuttingProgressMax
        });

        if (cuttingProgress >= recipe.cuttingProgressMax)
        {
            KitchenObjSO outputSO = GetRecipeOutput(kitchenObj.GetKitchenObjSO());
            if (!outputSO)
                return;
            
            kitchenObj.DestroySelf();
            KitchenObj.SpawnKitchenObj(outputSO, this);
        }
    }

    private bool HasRecipe(KitchenObjSO inputSO)
    {
        CuttingRecipeSO recipe = GetRecipe(inputSO);
        return recipe!=null;
    }

    private KitchenObjSO GetRecipeOutput(KitchenObjSO inputSO)
    {
        CuttingRecipeSO recipe = GetRecipe(inputSO);
        if(recipe)
            return recipe.output;
        
        return null;
    }

    private CuttingRecipeSO GetRecipe(KitchenObjSO inputSO)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipes)
        {
            if (recipe.input == inputSO)
                return recipe;
        }
        return null;
    }
}
