using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : MonoBehaviour
{
    private List<Transform> targets = new List<Transform> ();
    private Transform primaryTarget;
    [SerializeField] private float TimeBetweenAttacks = 2f;
    private Enemy enemyScript;
    private float damage = 50.0f;

    private bool isWaiting = false;

    private void Update()
    {
        //if the defence has a target
        if (primaryTarget != null && primaryTarget.tag == "Enemy")
        {
            //keep rotating the defence in the direction of the target (the enemy)
            Quaternion rotation = Quaternion.LookRotation(primaryTarget.transform.position - transform.position, transform.TransformDirection(Vector3.forward));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }

        if (!isWaiting && primaryTarget != null)
        {
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
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
    IEnumerator Attack()
    {
        enemyScript = primaryTarget.gameObject.GetComponent<Enemy>();
        //Debug.Log(primaryTarget);
        if (enemyScript != null){
            //enemyScript = primaryTarget.gameObject.GetComponent<Enemy>();
            enemyScript.Damage(damage);
        }

        //done so the coroutine only is called every time the last one ends
        isWaiting = true;
        yield return new WaitForSeconds(TimeBetweenAttacks);
        isWaiting = false;
    }
}