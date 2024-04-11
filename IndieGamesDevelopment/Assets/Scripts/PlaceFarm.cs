using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlaceFarm : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private GameObject FarmPrefab;
    [SerializeField] private Points pointScript;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuildFarm()
    {
        Instantiate(FarmPrefab, Vector3.zero, transform.rotation);
        pointScript.NumberOfFarms += 1;
        Debug.Log(pointScript.NumberOfFarms);
    }
}
