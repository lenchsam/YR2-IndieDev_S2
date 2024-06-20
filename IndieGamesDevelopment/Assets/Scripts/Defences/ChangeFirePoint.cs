using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeFirePoint : MonoBehaviour
{
    public GameObject firePoint;
    [SerializeField] private Button escapeButton;
    [SerializeField] private TMP_Text ScreenText;
    [SerializeField] private GameObject defenceMenu;
    private bool initialize = true;
    [SerializeField] private bool fromUI = false;
    [SerializeField] private clickDefence clickDefenceScript;

    // Update is called once per frame
    void Update()
    {
        if ( firePoint != null )
            ChangeFirePosition();
    }
    public void ChangeFirePosition()
    {
        defenceMenu.SetActive( false );
        //if touched something
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            //fire raycast to world position of player touch
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, Mathf.Infinity);

            //Debug.Log(hit.collider.name);
            //if hit something
            if (hit.collider != null)
            {
                firePoint.transform.position = Camera.main.ScreenToWorldPoint(touch.position);
                //Gizmos.DrawWireSphere(firePoint.transform.position, 1);
                escapeButton.gameObject.SetActive(true);
                ScreenText.text = "";
                if (!initialize && fromUI)
                    defenceMenu.SetActive(true);
                gameObject.SetActive(false);
                initialize = false;
                fromUI = false;
            }
        }
        clickDefenceScript.gameObject.SetActive(true);
    }
    public void setFromUI(bool option) 
    {
        fromUI = option;
    }
}
