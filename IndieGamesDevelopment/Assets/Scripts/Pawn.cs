using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Pawn : MonoBehaviour
{
    [SerializeField] private WaveScriptableObject pawnLocations;
    //[SerializeField] private AmountOfPawnsScriptableObject SO_PawnAmount;
    [SerializeField] private Button continueToWaveButton;
    private Animator anim;
    private bool gotPosition = false;
    private Transform moveTo;
    private bool atHouse;
    private bool isBuilding = false;
    private bool newRound = false;
    private List<Transform> houseLocations = new List<Transform>();

    [HideInInspector] public bool goingToBuild = false;
    [HideInInspector] public Vector2 buildLocation;
    [SerializeField] private float movementSpeed = 3f;

    void Start()
    {
        continueToWaveButton = GameObject.Find("NextWave").GetComponent<Button>();
        anim = GetComponent<Animator>();
        AddHouseLocations(GameObject.Find("HouseParent").transform, ref houseLocations);

        continueToWaveButton.onClick.AddListener(() => stoppedBuilding()); //listen to button click.
    }
    
    void Update()
    {
        if (!atHouse)
        {
            goToHouse();
        }

        if (goingToBuild)
        {
            goToBuild(buildLocation);
        }
        if (isBuilding)
            anim.SetBool("isBuilding", true);
        if(newRound)
        {
            goToHouse();
            //Debug.Log("ADSFLKNASVDBJASDFJ");
            if (!pawnLocations.gameObjectList.Contains(gameObject))
            {
                pawnLocations.gameObjectList.Add(gameObject);
            }
            //pawnLocations.gameObjectList.Add(gameObject);
            anim.SetBool("isBuilding", false);
        }
    }
    private void goToHouse()
    {
        if (!gotPosition)
        {
            moveTo = houseLocations[Random.Range(0, houseLocations.Count)];
            gotPosition = true;
        }
        goTo(moveTo);
    }
    public void goToBuild(Vector2 buildLocation)
    {
        //Debug.Log("GOING TO BUILD!!!!!!!!!!!!!");
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, buildLocation, step);

        if (Vector3.Distance(transform.position, buildLocation) < 0.001f)
        {
            //Debug.Log("CURRENTLY BUILDING");
            isBuilding = true;
        }
    }
    private void goTo(Transform moveTo)
    {
        //vectro3 move towards
        Debug.Log("moving towards house");
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, moveTo.position, step);

        if (Vector3.Distance(transform.position, moveTo.position) < 0.001f)
        {
            atHouse = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (newRound)
            {
                newRound = false;
            }
        }
    }
    private void AddHouseLocations(Transform parent, ref List<Transform> list)
    {
        foreach (Transform child in parent)
        {
            list.Add(child);
            AddHouseLocations(child, ref list);
        }
    }
    private void stoppedBuilding()
    {
        //Debug.Log("EVENT HAS BEEN LISTENED TO");
        isBuilding = false;
        goingToBuild = false;
        newRound = true;
    }
}
