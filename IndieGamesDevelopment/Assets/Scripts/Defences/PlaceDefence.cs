using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaceDefence : MonoBehaviour
{
    [SerializeField] private GameObject DefencePrefab;
    [SerializeField] private Points pointScript;
    [SerializeField] private int DefenceCost = 10;
    [SerializeField] private DefenceManager DM;

    private GameManager gameManager;

    private void Start()
    {
        //gameManager = 
        //pointScript = GameObject.Find("PointsUpdator").GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        //if touched something
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && pointScript.totalPoints >= 10)
        {
            Touch touch = Input.GetTouch(0);

            //fire raycast to world position of player touch
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

            //if hit something
            if (hit.collider != null)
            {
                //instantiate defence and update total points
                GameObject instantiatedObject = Instantiate(DefencePrefab, hit.point, transform.rotation);
                //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                //Instantiate(DefencePrefab, hit.point, transform.rotation);
                DM.addDefence(instantiatedObject);
                pointScript.totalPoints -= DefenceCost;
                pointScript.UpdatePointsText();
            }
        }

    }
}
