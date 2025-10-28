using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjSO", menuName = "Scriptable Objects/KitchenObjSO")]
public class KitchenObjSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objName;
}
