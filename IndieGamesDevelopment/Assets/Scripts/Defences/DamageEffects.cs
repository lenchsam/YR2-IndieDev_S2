using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffects : MonoBehaviour
{
    [Header("Fire Effect")]
    [SerializeField] private int FireDamage;
    [SerializeField] private int TimeBetweenDamage;

    [Header("Shadow Effect")]
    [SerializeField] private float SpawnRange;
    [SerializeField] private GameObject PrefabWarrior;
    [SerializeField] private LayerMask LM;

    private bool isWaiting = false;
    public void DEShadow(Vector3 center)
    {
        Transform positionToSpawn = GetSpawnPosition(center, SpawnRange);
        Instantiate(PrefabWarrior, positionToSpawn.position, transform.rotation);
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
    private Transform GetSpawnPosition(Vector3 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius, LM);
        int rand = Random.Range(0, hitColliders.Length);
        //Debug.Log("HIT " + hitColliders.Length);
        return hitColliders[rand].transform;
    }
}
