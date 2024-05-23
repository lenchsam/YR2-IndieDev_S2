using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefenceManager : MonoBehaviour
{
    public List<GameObject> Defences = new List<GameObject>();
    public List<Vector3> DefencePositions = new List<Vector3>();
    [SerializeField] private GameObject Turret;
    [SerializeField] private GameObject Morter;
    [SerializeField] private TMP_Text totalBuilders;
    [SerializeField] private WaveScriptableObject pawnLocations;
    [SerializeField] private AmountOfPawnsScriptableObject SO_PawnAmount;
    private void Start()
    {
        updateBuilderText();
    }
    public void addDefence(GameObject defence)
    {
        if(defence.name == "Turret(Clone)")
            Defences.Add(Turret);
        else
            Defences.Add(Morter);

        DefencePositions.Add(defence.transform.position);
    }

    public void placeAllDefences()
    {
        foreach (GameObject defence in Defences)
        {
            
            foreach(Vector3 defencePosition in DefencePositions)
            {
                Instantiate(defence, defencePosition, Quaternion.identity);
                
            }
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level")
            placeAllDefences();
    }
    public void updateBuilderText()
    {
        Debug.Log("EVENT CALLLLLLEEEDDDD");
        totalBuilders.text = pawnLocations.gameObjectList.Count.ToString() + "/" + SO_PawnAmount.amountOfPawns.ToString();
    }
}
