using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morter : DefenceDefault
{
    [Header("Specific Defence Variables")]
    [SerializeField] private Transform firePosition;
    [SerializeField] private float damageRadius;
    [SerializeField] private ContactFilter2D contactFilter = new ContactFilter2D();
    
    List<Collider2D> results = new List<Collider2D>();

    private bool isWaiting = false;

    public void Update()
    {
        if (isWaiting == false)
        {
            Debug.Log("FIRE");
            StartCoroutine(fire());
        }
    }

    IEnumerator fire()
    {
        explosiveDamage(damageRadius, new Vector2(firePosition.position.x, firePosition.position.y), contactFilter, ref results);
        //Debug.Log(results);
        results.Clear();
        isWaiting = true;
        yield return new WaitForSeconds(fireRate);
        isWaiting = false;
    }
}
