using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            moveTowardsPosition(points[counter].position);
            //var step = movementSpeed * Time.deltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, points[counter].position, step);
        }
    }
}
