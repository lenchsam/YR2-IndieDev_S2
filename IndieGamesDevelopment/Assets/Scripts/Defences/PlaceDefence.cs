using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    [SerializeField] private AmountOfPawnsScriptableObject SO_PawnAmount;
    [SerializeField] private DefenceManager S_DefenceManager;
    [SerializeField] private TextOnScreenScriptableObjects SO_Text;
    [SerializeField] private TextOnScreen S_TextOnScreen;
    [SerializeField] private GameObject GO_ChangeFirePoint;
    [SerializeField] private GameObject[] everythingToSetInactive;

    [Header("rpices")]
    [SerializeField] private int pawnPrice = 10;
    [SerializeField] private int turretPrice = 20;
    [SerializeField] private int morterPrice = 30;

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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                SetInactive();
                return;
            }

            //fire raycast to world position of player touch
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, Mathf.Infinity, LM);

            //if hit something
            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                SetInactive();
                GameObject instantiatedObject = Instantiate(DefencePrefab, hit.point, transform.rotation);
                //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                //Instantiate(DefencePrefab, hit.point, transform.rotation);
                if (instantiatedObject.name.Substring(0,4) != "Pawn")
                {
                    DM.addDefence(instantiatedObject);
                    savePosition(instantiatedObject);

                    if (SO_pawnLocations.gameObjectList.Count > 0) //if none of the elements in the list have a sprite renderer active
                    {
                        //Check if they have enough money to build
                        if ((instantiatedObject.name.Substring(0, 6) == "Turret" && pointScript.totalPoints < turretPrice) || (instantiatedObject.name.Substring(0, 6) == "Morter" && pointScript.totalPoints < morterPrice))
                        {
                            Debug.Log("NOT ENOUGH MONEY");
                            Destroy(instantiatedObject);
                            //pointScript.totalPoints += DefenceCost;
                            //pointScript.UpdatePointsText();
                            return;
                        }
                        //send pawn to build defence
                        //Debug.Log("making pawn go to build");
                        pawnScript = SO_pawnLocations.gameObjectList[Random.Range(0, SO_pawnLocations.gameObjectList.Count)].GetComponent<Pawn>();
                        SO_pawnLocations.gameObjectList.Remove(pawnScript.gameObject); //remove pawn from list so it isnt picked again to build a defence
                        pawnScript.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                        pawnScript.goingToBuild = true;
                        pawnScript.buildLocation = hit.point;
                        //pawnScript.goToBuild(instantiatedObject.transform.position);

                        S_DefenceManager.updateBuilderText();

                        //display mortar text
                        if (instantiatedObject.name.Substring(0, 6) == "Morter")
                        {
                            //Debug.Log("MAKING TEXT APPEARRRRRRR");
                            S_TextOnScreen.makeTextAppear(SO_Text);
                            GO_ChangeFirePoint.SetActive(true);
                            GO_ChangeFirePoint.GetComponent<ChangeFirePoint>().firePoint = instantiatedObject.transform.GetChild(5).gameObject;
                            gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        Destroy(instantiatedObject);
                        pointScript.totalPoints += DefenceCost;
                        pointScript.UpdatePointsText();
                    }
                }
                else
                {

                    SetInactive();
                    if (pointScript.totalPoints < pawnPrice)
                    {
                        Destroy(instantiatedObject);
                        return;
                    }
                    //Debug.Log("building pawn");
                    //deploy pawn to build
                    SO_PawnAmount.amountOfPawns++;
                    SO_pawnLocations.gameObjectList.Add(instantiatedObject);
                    S_DefenceManager.updateBuilderText();
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
        //Debug.Log(GOToAdd.name);
        if (this.name == "----PlacePawn----")
        {
            //Debug.Log("addding to gameobejct list");
            SO_pawnLocations.gameObjectList.Add(GOToAdd);
        }
    }
    private void SetInactive()
    {
        foreach (GameObject GO in everythingToSetInactive)
        {
            GO.SetActive(false);
        }
    }
}
