using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "Scriptable Objects/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjSO input;
    public KitchenObjSO output;
    public float fryingTimerMax = 2;
}
