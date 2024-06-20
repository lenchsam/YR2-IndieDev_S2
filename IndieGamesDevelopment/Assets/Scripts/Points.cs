using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour
{
    [Header("Basic Settings")]
    public int totalPoints = 0;

    private TMP_Text _pointsText;
    //private TMP_Text _PointsPerRoundText;
    private FarmingManager _farmingManager;
    //making the gameobject stay between scenes

    public bool nextRound = false;

    private void Start()
    {
        //gets all components needed to run the script
        _pointsText = GameObject.Find("AmmountOfPoints").GetComponent<TMP_Text>();
        //_PointsPerRoundText = GameObject.Find("PointsPerRound").GetComponent<TMP_Text>();

        UpdatePointsText();
    }
    //private void OnLevelWasLoaded(int level)
    //{
    //    //if scene isnt main menu then find ammount of points text
    //    Scene scene = SceneManager.GetActiveScene();
    //    if (scene.name != "Main Menu")
    //    {
    //        _pointsText = GameObject.Find("AmmountOfPoints").GetComponent<TMP_Text>();
    //        //_PointsPerRoundText = GameObject.Find("PointsPerRound").GetComponent<TMP_Text>();
    //    }


    //    UpdatePointsText();
    //}
    //function called in the farming script after the points are updated
    //This function changed the text to the new amount of points
    public void UpdatePointsText()
    {
        //Debug.Log("changed points text");
        _pointsText.text = totalPoints.ToString();
        //_PointsPerRoundText.text = (_farmingManager._farmNumber * _farmingManager.PointsPerFarm).ToString();
    }
    public void UpdateTotalPoints(int PointsToGive)
    {
        //Debug.Log(PointsToGive);
        totalPoints += PointsToGive;
        //Debug.Log("total points are now" + totalPoints);
        UpdatePointsText();
    }
}
