using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Morter : ExplosiveDefence
{
    [Header("Specific Defence Variables")]
    [SerializeField] private Transform firePosition;
    [SerializeField] private float damageRadius;
    [SerializeField] private ContactFilter2D contactFilter = new ContactFilter2D();
    [SerializeField] private GameObject chooseFirePosition;
    
    List<Collider2D> results = new List<Collider2D>();

    private bool isWaiting = false;

    private void Start()
    {
        Effects = GameObject.Find("----DamageEffects----").GetComponent<DamageEffects>();
        chooseFirePosition = GameObject.Find("----ChangeFirePosition----");
        chooseFirePosition.SetActive(true);
    }
    public void Update()
    {
        if (isWaiting == false)
        {
            //Debug.Log("FIRE");
            StartCoroutine(fire());
        }
    }

    IEnumerator fire()
    {
        DTExplosive(damageRadius, new Vector2(firePosition.position.x, firePosition.position.y), contactFilter, ref results);
        results.Clear();
        isWaiting = true;
        yield return new WaitForSeconds(fireRate);
        isWaiting = false;
    }
}
