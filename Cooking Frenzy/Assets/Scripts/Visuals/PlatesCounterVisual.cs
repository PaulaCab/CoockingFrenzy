using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform topPoint;
    [SerializeField] private GameObject plateVisual;

    private List<GameObject> plateVisualsList = new List<GameObject>();

    private void Start()
    {
        if (platesCounter)
        {
            platesCounter.OnPlateSpawn += Counter_OnPlateSpawn;
            platesCounter.OnPlateRemove += Counter_OnPlateRemove;
        }
    }

    private void Counter_OnPlateRemove(object sender, EventArgs e)
    {
        GameObject plate = plateVisualsList[plateVisualsList.Count - 1];
        plateVisualsList.Remove(plate);
        Destroy(plate);
    }

    private void Counter_OnPlateSpawn(object sender, EventArgs e)
    {
        GameObject plate = Instantiate(plateVisual, topPoint);

        float offset = 0.1f;
        plate.transform.localPosition = new Vector3(0, offset * plateVisualsList.Count, 0);
        plateVisualsList.Add(plate);
    }
}
