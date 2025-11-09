using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private GameObject iconTemplate;

    private void Start()
    {
        if (plate)
            plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (KitchenObjSO ingredient in plate.GetIngredientList())
        {
            GameObject icon = Instantiate(iconTemplate, transform);
            icon.GetComponent<IconUI>().SetKitchenObject(ingredient);
        }
    }
}
