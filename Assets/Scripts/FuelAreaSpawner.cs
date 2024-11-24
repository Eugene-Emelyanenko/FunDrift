using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelAreaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] fuelAreaPrefabs;
    public Vector2 offset;

    public void SpawnFuelArea(Vector2 pos)
    {
        Instantiate(fuelAreaPrefabs[Random.Range(0, fuelAreaPrefabs.Length)], pos + offset, Quaternion.identity);
    }
}
