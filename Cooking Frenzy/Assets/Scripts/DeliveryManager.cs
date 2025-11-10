using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    public event EventHandler<OnRecipeEventArgs> OnRecipeSpawn;
    public event EventHandler<OnRecipeEventArgs> OnRecipeComplete;
    public class OnRecipeEventArgs : EventArgs
    {
        public RecipeSO recipeSO;
    }
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO allRecipeList;
    private List<RecipeSO> waitingRecipeList = new List<RecipeSO>();
    public List<RecipeSO> GetWaitingRecipeList(){return waitingRecipeList;}

    public DeliveryCounter deliveryCounter;
    private float spawnTimer = 0;
    [SerializeField] private float spawnTimerMax = 4f;
    [SerializeField] private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnTimerMax;

            if (waitingRecipeList.Count < waitingRecipesMax)
            {
                RecipeSO recipe = allRecipeList.recipeList[Random.Range(0, allRecipeList.recipeList.Count)];  
                waitingRecipeList.Add(recipe);
                OnRecipeSpawn?.Invoke(this, new OnRecipeEventArgs{recipeSO = recipe});
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plate)
    {
        foreach (RecipeSO recipe in waitingRecipeList)
        {
            if (CheckRecipeMatch(recipe, plate))
            {
                waitingRecipeList.Remove(recipe);
                OnRecipeComplete?.Invoke(this, new OnRecipeEventArgs{recipeSO = recipe});
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
    }

    private bool CheckRecipeMatch(RecipeSO recipe, PlateKitchenObject plate)
    {
        if (recipe.ingredientList.Count != plate.GetIngredientList().Count)
            return false;

        bool matchRecipe = true;
        foreach (KitchenObjSO recipeKitchenObjSO in recipe.ingredientList)
        {
            bool found = false;
            foreach (var plateKitchenObjSO in plate.GetIngredientList())
            {
                if (plateKitchenObjSO == recipeKitchenObjSO)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                matchRecipe = false;
        }

        return matchRecipe;
    }
}
