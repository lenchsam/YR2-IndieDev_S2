using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceDefence : MonoBehaviour
{
    [SerializeField] private GameObject DefencePrefab;
    [SerializeField] private Points pointScript;
    [SerializeField] private int DefenceCost = 10;
    [SerializeField] private DefenceManager DM;
    [SerializeField] private LayerMask LM;
    private AudioManager AM;
    [SerializeField] private AudioClip rejectAudio;
    [SerializeField] private WaveScriptableObject SO_pawnLocations;

    private Pawn pawnScript;

    private GameManager gameManager;

    private void Start()
    {
        AM = GameObject.Find("----AudioManager----").GetComponent<AudioManager>();
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, Mathf.Infinity, LM);

            //if hit something
            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                //instantiate defence and update total points
                GameObject instantiatedObject = Instantiate(DefencePrefab, hit.point, transform.rotation);
                //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                //Instantiate(DefencePrefab, hit.point, transform.rotation);
                if (instantiatedObject.name.Substring(0,4) != "Pawn")
                {
                    Debug.Log("placing normal defence");
                    DM.addDefence(instantiatedObject);
                }
                else
                {
                    Debug.Log("placing pawn");
                    //deploy pawn to build
                    savePosition(instantiatedObject);
                    pawnScript = SO_pawnLocations.gameObjectList[Random.Range(0, SO_pawnLocations.gameObjectList.Count)].GetComponent<Pawn>();
                    pawnScript.goToBuild(hit.point);
                }
                pointScript.totalPoints -= DefenceCost;
                pointScript.UpdatePointsText();
            }
            else if (hit.collider != null && hit.collider.tag != "Ground")
            {
                //Debug.Log("playing sounds");
                AM.playSound(rejectAudio);
            }
        }
    }
    private void savePosition(GameObject GOToAdd)
    {
        Debug.Log(GOToAdd.name);
        if (this.name == "----PlacePawn----")
        {
            Debug.Log("addding to gameobejct list");
            SO_pawnLocations.gameObjectList.Add(GOToAdd);
        }
    }
}
