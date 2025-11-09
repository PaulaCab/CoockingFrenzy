using TMPro;
using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject iconsContainer;
    [SerializeField] private GameObject iconTemplate;
    private RecipeSO recipe;

    public RecipeSO GetRecipe() { return recipe;}
    public void SetRecipe(RecipeSO newRecipe)
    {
        recipe = newRecipe;
        nameText.SetText(recipe.recipeName);

        foreach (KitchenObjSO ingredient in recipe.ingredientList)
        {
            GameObject icon = Instantiate(iconTemplate, iconsContainer.transform);
            icon.GetComponent<IconUI>().SetKitchenObject(ingredient);
        }
    }
}
