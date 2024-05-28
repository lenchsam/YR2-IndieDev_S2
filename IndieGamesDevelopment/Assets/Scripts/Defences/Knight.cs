using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    //enemy goes into redius
    //knight starts attacking the enemy
    //disapears after a set time
    [Header("Base Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float movementSpeed;

    [Header("Animations")]
    [SerializeField] protected Animator anim;

    private List<Transform> targets = new List<Transform>();
    [SerializeField] private Transform primaryTarget;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Enemy")
            return;
        //this adds a new target to the list every time something enters the collider

        //Debug.Log("enemy entered collider");

        //primary target is always the next thing to enter the collider
        if (primaryTarget == null)
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

    void Update()
    {
        //moveTowardsEnemy
        if(primaryTarget != null)
        {
            //what angle is the enemy?
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(primaryTarget.position.y - transform.position.y, primaryTarget.position.x - transform.position.x));
            //Set the animation variables depending on what the angle of the enemy is
            anim.SetFloat("Angle", angle);
            anim.SetBool("isAttacking", true);
            flipBool();
            //moveTowardsPosition(primaryTarget.position);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }
    private void moveTowardsPosition(Vector3 targetPosition)
    {
        Debug.Log("moving");
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.GetChild(0).position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
    private void flipBool()
    {
        if (anim.GetBool("isTurnOne"))
        {
            anim.SetBool("isTurnOne", false);
        }
        else
        {
            anim.SetBool("isTurnOne", true);
        }
    }
}
