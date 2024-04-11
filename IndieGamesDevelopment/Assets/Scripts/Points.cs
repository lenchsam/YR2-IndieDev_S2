using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    [Header("Basic Settings")]
    public int totalPoints = 0;
    [SerializeField] private TMP_Text pointsText;

    [Space(2)]
    [Header("Farming")]
    [SerializeField] private bool isFarmingScene = false;
    [SerializeField] private float TimeToWait = 1f;
    [SerializeField] private int pointsPerFarm = 10;

    private float timePassed = 0f;
    [HideInInspector] public int NumberOfFarms;
    private void Start()
    {
        UpdatePointsText();
    }
    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > TimeToWait)
        {
            UpdateTotalPoints(pointsPerFarm * NumberOfFarms);
            timePassed = 0f;
        }
    }
    //function called in the farming script after the points are updated
    //This function changed the text to the new amount of points
    public void UpdatePointsText()
    {
        //Debug.Log("changed points text");
        pointsText.text = totalPoints.ToString();
    }
    public void UpdateTotalPoints(int PointsToGive)
    {
        //Debug.Log(PointsToGive);
        totalPoints += PointsToGive;
        //Debug.Log("total points are now" + totalPoints);
        UpdatePointsText();
    }
}
