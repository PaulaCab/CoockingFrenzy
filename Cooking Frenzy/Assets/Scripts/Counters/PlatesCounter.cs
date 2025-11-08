using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;
    
    [SerializeField] private KitchenObjSO plateSO;
    [SerializeField] float spawnTimerMax = 4f;
    private float spawnTimer = 0f;
    private int platesAmountMax = 4;
    private int platesAmount;
    
    

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0f;
            if (platesAmount < platesAmountMax)
            {
                platesAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObj())
        {
            if (platesAmount > 0)
            {
                platesAmount--;
                KitchenObj.SpawnKitchenObj(plateSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
