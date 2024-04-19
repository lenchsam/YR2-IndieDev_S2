using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : DefenceDefault
{
    private List<Transform> targets = new List<Transform>();
    private Transform primaryTarget;
    private float time;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Enemy")
            return;
        //this adds a new target to the list every time something enters the collider

        //Debug.Log("enemy entered collider");

        //primary target is always the next thing to enter the collider
        primaryTarget = collider.gameObject.transform;

        //Debug.Log(collision.gameObject.transform);
        targets.Add(collider.transform);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("enemy exit collider");

        //if what left the collider was the primary target, reset primary target to null.
        if (collider.transform == primaryTarget)
            primaryTarget = null;

        //removes whatever left the collider from the list
        targets.Remove(collider.transform);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (primaryTarget == null)
            nextTarget();

        //rotates the defence to the target
        if (primaryTarget != null)
        {
            rotateDefenceToTarget("Enemy", primaryTarget);
            //if a set amount of time has gone, it attacks the primary target
            if (time >= fireRate)
            {
                Debug.Log("done damage");
                damageSingular(primaryTarget.gameObject, damage);
                time = 0;
            }
        }
    }
    private void nextTarget()
    {
        if (targets.Count != 0)
            primaryTarget = targets[0];
    }
}
