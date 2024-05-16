using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [Header("Fire Effect")]
    [SerializeField] private int FireDamage;
    [SerializeField] private int TimeBetweenDamage;
    [Header("Shadow Effect")]
    [Range(1f, 3.0f)]
    [SerializeField] private float AmountOfSlow;

    private bool isWaiting = false;
    public void DEShadow(DefaultEnemy enemy)
    {
        //SpriteRenderer sr = enemy.gameObject.GetComponent<SpriteRenderer>();
        //sr.color = new Color(0.3f, 0.5f, 0.8f);
        //enemy.movementSpeed *= AmountOfSlow;
    }
    public void DEFire(DefaultEnemy enemy)
    {
        //Debug.Log("FIRE BEING CALLED");
        if (enemy == null)
            return;
        //Debug.Log("DOING FIRE DAMAGE");
        if (!isWaiting)
            StartCoroutine(FireTick(enemy));
    }
    IEnumerator FireTick(DefaultEnemy enemy)
    {
        enemy.Damage(FireDamage);

        isWaiting = true;
        yield return new WaitForSeconds(TimeBetweenDamage);
        isWaiting = false;
    }
}
