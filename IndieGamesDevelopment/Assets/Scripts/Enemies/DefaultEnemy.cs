using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : EnemyDefault
{
    // Start is called before the first frame update
    void Start()
    {
        done = true;
        counter = 1;

        GameObject pathParent = GameObject.Find("PathToTake");
        //get all path points
        AddDescendants(pathParent.transform, ref points);

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //lerp position of enemy
        if (done)
        {
            //Debug.Log("lerp");
            StartCoroutine(LerpPosition(points[counter].position, movementSpeed));
        }
    }
}
