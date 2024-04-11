using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    [Header("Basic Settings")]
    public int totalPoints = 0;

    [Space(2)]
    [Header("Farming")]
    [SerializeField] private int pointsPerFarm = 10;

    [HideInInspector] public int NumberOfFarms;
    private TMP_Text pointsText;
    private GameObject temp;
    //making the gameobject stay between scenes
    static Points instance;

    public bool nextRound = true;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        temp = GameObject.Find("AmmountOfPoints");
        pointsText = temp.GetComponent<TMP_Text>();
        UpdatePointsText();
    }
    private void Update()
    {
        if (pointsText == null)
        {
            temp = GameObject.Find("AmmountOfPoints");
            if (temp != null)
            {
                pointsText = temp.GetComponent<TMP_Text>();
            }
            UpdatePointsText();
        }

        if (nextRound)
        {
            UpdateTotalPoints(pointsPerFarm * NumberOfFarms);
            nextRound = false;
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
