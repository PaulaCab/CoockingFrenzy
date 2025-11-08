using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjSO kitchenObjSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private List<KitchenObjectSO_GameObject> equivalenceList;

    private void Start()
    {
        if (plate)
            plate.OnIngredientAdded += Plate_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject equivalence in equivalenceList)
        {
            equivalence.gameObject.SetActive(false);
        }
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject equivalence in equivalenceList)
        {
            if(equivalence.kitchenObjSO == e.objSO)
                equivalence.gameObject.SetActive(true);
        }
    }
}
