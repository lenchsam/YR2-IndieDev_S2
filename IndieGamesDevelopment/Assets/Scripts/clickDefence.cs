using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static DefenceDefault;
using UnityEditor.Animations;
using System.IO;

public class clickDefence : MonoBehaviour
{
    private MainMenu menuScript;
    //[SerializeField] private GameObject turretPrefab;
    [SerializeField] private TMP_Text defenceText;
    [SerializeField] private TMP_Dropdown effectsDropdown;
    [SerializeField] private GameObject clickedDefenceUI;
    [SerializeField] private LayerMask LM;
    [SerializeField] private GameObject ChooseFirePosition;

    string selectedOption; //will use to set the dropdown to a certain value
    Collider2D theHitObject;

    private GameObject selectedDefence;
    private Turret turretScript;
    private Morter MorterScript;
    [SerializeField] private ChangeFirePoint ChangeFirePointScript;
    //Start is called before the first frame update
    void Start()
    {
        menuScript = GetComponent<MainMenu>();
        addOptionsToDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        //if touched something
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            //fire raycast to world position of player touch
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, Mathf.Infinity, LM);
            
            //if hit something
            if (hit.collider != null && hit.collider.gameObject.tag == "Defence")
            {
                selectedDefence = hit.collider.gameObject;
                //detect what enemy has been clicked
                if (hit.collider.gameObject.name == "Turret(Clone)")
                {
                    theHitObject = hit.collider;
                    turretScript = hit.collider.gameObject.GetComponentInChildren<Turret>();
                    ChooseFirePosition.SetActive(false);
                }
                else if (hit.collider.gameObject.name == "Morter(Clone)")
                {
                    theHitObject = hit.collider;
                    MorterScript = hit.collider.gameObject.GetComponentInChildren<Morter>();
                    ChooseFirePosition.SetActive(true);
                    ChangeFirePointScript.firePoint = theHitObject.transform.GetChild(0).gameObject;
                }
                menuScript.ToggleUI(); // enable ui for the defence

                int val = effectsDropdown.value;
                selectedOption = effectsDropdown.options[val].text;

                //set the correct dropdown value to the currently selected damage type
                effectsDropdown.value = getCurrentEffect();

                //change all variables in the defence to suit the current defence
                defenceText.text = displayName(hit.collider.gameObject.name);//change name
            }
        }
    }
    private string displayName(string defenceName)
    {
        string temp;
        string cleanDefenceName = "";
        for (int i = 0; i < defenceName.Length; i++)
        {
            temp = defenceName.Substring(i, 1);
            if (temp == "(")
            {
                return cleanDefenceName;
            }
            else
            {
                cleanDefenceName += temp;
            }
        }
        return cleanDefenceName;
    }
    private void addOptionsToDropdown()
    {
        List<string> m_DropOptions = new List<string>();

        string[] effectTypeNames = System.Enum.GetNames(typeof(effectType));
        for (int i = 0; i < effectTypeNames.Length; i++)
        {
            m_DropOptions.Add(effectTypeNames[i]);
        }
        Debug.Log(m_DropOptions);
        effectsDropdown.AddOptions(m_DropOptions);
    }
    //called every time the user changes the dropdown menu value
    private void DropdownValueChanged(TMP_Dropdown change, Collider2D defence)
    {
        Turret turretScript;
        Morter morterScript;
        string currentlySelected;
        if (defence.gameObject.name == "Turret(Clone)")
        {
            turretScript = defence.gameObject.GetComponentInChildren<Turret>();

            currentlySelected = effectsDropdown.options[effectsDropdown.value].text;
            if (currentlySelected == "None")
            {
                turretScript.typeOfEffect = effectType.None;
            }
            else if (currentlySelected == "Fire")
            {
                turretScript.typeOfEffect = effectType.Fire;
            }
            else if (currentlySelected == "Shadow")
            {
                turretScript.typeOfEffect = effectType.Shadow;
            }
            turretScript.instantiateAura(defence);
        }else if (defence.gameObject.name == "Morter(Clone)")
        {
            morterScript = defence.gameObject.GetComponentInChildren<Morter>();

            currentlySelected = effectsDropdown.options[effectsDropdown.value].text;
            if (currentlySelected == "None")
            {
                morterScript.typeOfEffect = effectType.None;
            }
            else if (currentlySelected == "Fire")
            {
                morterScript.typeOfEffect = effectType.Fire;
            }
            else if (currentlySelected == "Shadow")
            {
                morterScript.typeOfEffect = effectType.Shadow;
            }
            morterScript.instantiateAura(defence);
        }
    }
    public void ValueChanged()
    {
        DropdownValueChanged(effectsDropdown, theHitObject);
    }
    private int getCurrentEffect()
    {
        //Debug.Log("DEFENCES NAME = " + selectedDefence.name);
        GameObject[] children = new GameObject[selectedDefence.transform.childCount];

        //Get list of all children
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = selectedDefence.transform.GetChild(i).gameObject;
            if (children[i].gameObject.activeSelf && children[i].tag != "Prefab")
            {
                //is the currently active 
                Debug.Log(children[i].name);
                return i-1;
            }
        }
        return 0;
    }
}
