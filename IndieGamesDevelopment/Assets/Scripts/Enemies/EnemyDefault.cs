using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private EnemyCounterScriptableObject SO_EnemyCounter;

    [SerializeField] protected Spawner spawnerScript;

    protected void moveTowardsPosition(Vector3 targetPosition)
    {
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

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
            SO_EnemyCounter.numberOfEnemies--;
            if(SO_EnemyCounter.numberOfEnemies == 0)
                spawnerScript.E_waveFinished.Invoke();
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
