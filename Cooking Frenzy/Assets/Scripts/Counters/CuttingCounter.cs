using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnCut; 
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
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
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{progressNormalized = 0});
            }
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
            }
            else
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
        OnCut?.Invoke(this, EventArgs.Empty);
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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
