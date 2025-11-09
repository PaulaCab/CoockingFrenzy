using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Objects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    [SerializeField] public string recipeName;
    public List<KitchenObjSO> ingredientList;
}
