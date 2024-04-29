using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeFirePoint : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;

    // Update is called once per frame
    void Update()
    {
        ChangeFirePosition();
    }
    public void ChangeFirePosition()
    {
        //if touched something
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            //fire raycast to world position of player touch
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, Mathf.Infinity);

            //if hit something
            if (hit.collider != null)
            {
                firePoint.transform.position = hit.collider.transform.position;
                Debug.Log("changed fire position");
            }
        }
    }
}
