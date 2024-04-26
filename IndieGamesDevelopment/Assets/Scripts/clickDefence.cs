using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using static GameManager;
using static DefenceDefault;

public class clickDefence : MonoBehaviour
{
    private MainMenu menuScript;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private TMP_Text defenceText;
    [SerializeField] private TMP_Dropdown effectsDropdown;
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

            //if hit something
            if (hit.collider != null && hit.collider.gameObject.tag == "Defence")
            {
                turretScript = hit.collider.gameObject.GetComponentInChildren<Turret>();
                Debug.Log("Clicked Defence");
                menuScript.ToggleUI(); // enable ui for the defence

                //change all variables in the defence to suit the current defence
                defenceText.text = displayName(hit.collider.gameObject.name);//change name
                //add the effects to the dropdown menu
                //addOptionsToDropdown();

                //listens to whenever the value of the dropdown menu is changed
                effectsDropdown.onValueChanged.AddListener(delegate {
                    turretScript.deleteAura();
                    DropdownValueChanged(effectsDropdown, hit.collider);
                    
                }); //https://docs.unity3d.com/2018.4/Documentation/ScriptReference/UI.Dropdown-onValueChanged.html
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
        turretScript.instantiateAura();
        //change the effect type here
    }
}
