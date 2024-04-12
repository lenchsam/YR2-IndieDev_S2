using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Farming : MonoBehaviour
{
    private int pointsToGive = 10;

    public Points pointScript; 

    void Start()
    {
        pointScript = GameObject.Find("----PointsUpdator----").GetComponent<Points>();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    //call every time wave changes
    public void points()
    {
        pointScript.UpdateTotalPoints(pointsToGive);
        //Debug.Log("gained points" + pointScript.totalPoints);
        pointScript.UpdatePointsText();
    }
}
