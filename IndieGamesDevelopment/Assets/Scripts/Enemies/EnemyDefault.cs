using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyDefault : MonoBehaviour
{
    [Header("Base Settings")]
    public float movementSpeed;
    [SerializeField] protected float Health;
    public float damage;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected List<Transform> points = new List<Transform>();
    
    protected int counter;
    protected bool done;

    [HideInInspector] public bool frozen = false;
    [SerializeField] protected int freezeTimer;

    protected void moveTowardsPosition(Vector3 targetPosition)
    {
        float time;

        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        //if the enemy hsa been frozen by a defence
        //if (frozen)
        //{
        //    time += Time.deltaTime;
        //    if (time >= freezeTimer)
        //    {
        //        //frozen = false;
        //    }
        //}

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            counter++;
        }
        //if the counter is equal to the list size
        if (counter == points.Count)
        {
            //stops all the coroutines and sets done to false so no more coroutines are called.
            done = false;
        }
    }
    //get all path points
    protected void AddDescendants(Transform parent, ref List<Transform> list)
    {
        foreach (Transform child in parent)
        {
            list.Add(child);
            AddDescendants(child, ref list);
        }
    }
    public void Damage(float damage)
    {
        Health -= damage;

        //changes the colour of the enemy depending on the enemies health
        ChangeColour();

        //is health is less than or equal to 0 destroy the enemy
        if (Health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    private void ChangeColour()
    {
        if (Health < 25)
        {
            sr.color = new Color(1f, 0f, 0f);
        }
        else if (Health <= 50)
        {
            sr.color = new Color(1f, 0.8f, 0f);
        }
        else if (Health < 75)
        {
            sr.color = new Color(1f, 1f, 0f);
        }
    }
    public float returnHealth()
    {
        return Health;
    }
}
