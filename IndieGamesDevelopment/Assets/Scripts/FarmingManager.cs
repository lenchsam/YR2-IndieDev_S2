using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingManager : MonoBehaviour
{
    private int _farmNumber = 0;
    public int  PointsPerFarm = 10;
    public Points pointScript;
    void Start()
    {
        pointScript = GameObject.Find("----PointsUpdator----").GetComponent<Points>();
    }
}
