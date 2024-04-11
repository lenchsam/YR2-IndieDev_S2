using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Farming : MonoBehaviour
{
    private int pointsToGive = 10;
    [SerializeField] private int timeBetweenPoints = 1;

    public Points pointScript; 

    private bool isWaiting = false;
    void Start()
    {
        pointScript = GameObject.Find("----PointsUpdator----").GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting == false)
        {
            StartCoroutine("points");
        }
    }
    IEnumerator points()
    {
        pointScript.UpdateTotalPoints(pointsToGive);
        //Debug.Log("gained points" + pointScript.totalPoints);
        pointScript.UpdatePointsText();
        isWaiting = true;
        yield return new WaitForSeconds(timeBetweenPoints);
        isWaiting = false;
    }
}
