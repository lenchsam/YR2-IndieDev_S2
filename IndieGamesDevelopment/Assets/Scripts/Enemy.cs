using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> targets = new List<Transform>();
    private float health = 100;
    public float damage = 20;
    private SpriteRenderer sr;
    //[SerializeField] private Transform startPosition;

    private bool done = true;
    private int counter = 1;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // check if the enemy has got to the position, if it has, start the coroutine again.
        if (done)
        {
            StartCoroutine(LerpPosition(targets[counter].position, 5));
        }
    }
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
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
        if (counter == targets.Count)
        {
            //stops all the coroutines and sets done to false so no more coroutines are called.
            done = false;
            StopAllCoroutines();
        }
    }
    public void Damage(float damage)
    {
        health -= damage;

        //changes the colour of the enemy depending on the enemies health
        ChangeColour();

        //is health is less than or equal to 0 destroy the enemy
        if (health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
    private void ChangeColour()
    {
        if (health < 25)
        {
            sr.color = new Color(1f, 0f, 0f);
        }else if (health <= 50)
        {
            sr.color = new Color(1f, 0.8f, 0f);
        }
        else if (health < 75)
        {
            sr.color = new Color(1f, 1f, 0f);
        }
    }
}
