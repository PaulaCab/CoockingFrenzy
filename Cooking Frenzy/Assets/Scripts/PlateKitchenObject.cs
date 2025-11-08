using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObj
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjSO objSO;
    }

    [SerializeField] private List<KitchenObjSO> validObjList;
    private List<KitchenObjSO> ingredientList = new List<KitchenObjSO>();
    
    public bool TryAddIngredient(KitchenObjSO ingredientSO)
    {
        if (!validObjList.Contains(ingredientSO))
            return false;
        
        if (ingredientList.Contains(ingredientSO))
            return false;
        
        ingredientList.Add(ingredientSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{objSO = ingredientSO});
        return true;
    }

}
