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
    protected IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        //done to setup everything needed for the lerp
        done = false;
        float time = 0;
        Vector3 startPosition = transform.position;
        //while the time done is less than the duration wanted
        while (time < duration)
        {
            //lerp the enemy
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            //Debug.Log("lerping" + transform.position);
            //add to the time
            time += Time.deltaTime;
            yield return null;
        }
        //lerp is finished
        done = true;
        //counter incremented so the enemy lerps to the next position in the list
        counter++;
        //if the counter is equal to the list size
        if (counter == points.Count)
        {
            //stops all the coroutines and sets done to false so no more coroutines are called.
            done = false;
            StopAllCoroutines();
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
