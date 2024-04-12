using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlaceFarm : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private GameObject FarmPrefab;
    [SerializeField] private Points pointScript;


    private FarmingManager _farmingManager;

    private void Start()
    {
        temp = GameObject.Find("----FarmingManager----");
        _farmingManager = temp.GetComponent<FarmingManager>();

        //Debug.Log(_farmingManager.ToString());
        //UpdatePointsText();

        temp = null;
    }

    private GameObject temp;

    // Update is called once per frame
    void Update()
    {
        if (pointScript == null)
        {
            temp = GameObject.Find("----PointsUpdator----");
            pointScript = temp.GetComponent<Points>();
        }
    }
    public void BuildFarm()
    {
        Instantiate(FarmPrefab, Vector3.zero, transform.rotation);
        pointScript.NumberOfFarms += 1;
        //Debug.Log(pointScript.NumberOfFarms);
    }
}
