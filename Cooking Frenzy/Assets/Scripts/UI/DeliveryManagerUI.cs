using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject recipeTemplate;

    private List<RecipeUI> recipeUIList = new List<RecipeUI>();
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn += Delivery_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeComplete += Delivery_OnRecipeComplete;
    }

    private void Delivery_OnRecipeComplete(object sender, DeliveryManager.OnRecipeEventArgs e)
    {
        foreach (RecipeUI recipeUI in recipeUIList)
        {
            if (recipeUI.GetRecipe() == e.recipeSO)
            {
                recipeUIList.Remove(recipeUI);
                Destroy(recipeUI.gameObject);
                break;
            }
        }
    }

    private void Delivery_OnRecipeSpawn(object sender, DeliveryManager.OnRecipeEventArgs e)
    {
        RecipeUI recipeUI = Instantiate(recipeTemplate, this.transform).GetComponent<RecipeUI>();
        recipeUIList.Add(recipeUI);
        recipeUI.SetRecipe(e.recipeSO);
    }

}
