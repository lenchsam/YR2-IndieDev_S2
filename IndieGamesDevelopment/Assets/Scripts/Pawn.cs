using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private ScriptableObject pawnLocations;

    private Animator anim;
    private bool gotPosition = false;
    private Transform moveTo;
    private bool atHouse;
    private bool isBuilding = false;
    private List<Transform> houseLocations = new List<Transform>();

    [Header("MovementSettings")]
    [SerializeField] private float movementSpeed = 3f;

    void Start()
    {
        anim = GetComponent<Animator>();
        AddHouseLocations(GameObject.Find("HouseParent").transform, ref houseLocations);
    }
    
    void Update()
    {
        if (!atHouse)
        {
            goToHouse();
        }

        if (isBuilding)
            anim.SetBool("isBuilding", true);
        else
        {
            goToHouse();
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
        Debug.Log("GOING TO BUILD!!!!!!!!!!!!!");
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, buildLocation, step);

        if (Vector3.Distance(transform.position, buildLocation) < 0.001f)
        {
            isBuilding = true;
        }
    }
    private void goTo(Transform moveTo)
    {
        //vectro3 move towards
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, moveTo.position, step);

        if (Vector3.Distance(transform.position, moveTo.position) < 0.001f)
        {
            gameObject.SetActive(false);
        }
    }
    protected void AddHouseLocations(Transform parent, ref List<Transform> list)
    {
        foreach (Transform child in parent)
        {
            list.Add(child);
            AddHouseLocations(child, ref list);
        }
    }
}
