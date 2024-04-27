using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static DefenceDefault;

public class clickDefence : MonoBehaviour
{
    private MainMenu menuScript;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private TMP_Text defenceText;
    [SerializeField] private TMP_Dropdown effectsDropdown;
    [SerializeField] private GameObject clickedDefence;
    [SerializeField] private LayerMask LM;

    private string oldValue = "None";
    string selectedOption;
    Collider2D theHitObject;

    private Turret turretScript;
    // Start is called before the first frame update
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
                theHitObject = hit.collider;
                turretScript = hit.collider.gameObject.GetComponentInChildren<Turret>();
                //Debug.Log("Clicked Defence");
                //if (clickedDefence.activeSelf == false)
                //{
                menuScript.ToggleUI(); // enable ui for the defence
                //}
                //else
                //{
                //    Debug.Log("defence already selected");
                //}
                 int val = effectsDropdown.value;
                oldValue = selectedOption;
                selectedOption = effectsDropdown.options[val].text;

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
        Turret turretScript = defence.gameObject.GetComponentInChildren<Turret>();
        string currentlySelected;

        currentlySelected = effectsDropdown.options[effectsDropdown.value].text;
        if (currentlySelected == "None")
        {
            turretScript.typeOfEffect = effectType.None;
        }else if (currentlySelected == "Fire")
        {
            turretScript.typeOfEffect = effectType.Fire;
        }
        else if (currentlySelected == "Freeze")
        {
            turretScript.typeOfEffect = effectType.Freeze;
        }
        Debug.Log(defence.gameObject);
        turretScript.instantiateAura(defence);
        //change the effect type here
        defence = null;
        turretScript = null;
    }
    public void ValueChanged()
    {
        DropdownValueChanged(effectsDropdown, theHitObject);
    }
    public void deleteAura()
    {
        turretScript.deleteAura();
    }
}
