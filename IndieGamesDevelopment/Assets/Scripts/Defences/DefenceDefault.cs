using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceDefault : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected damageType typeOfDamage = new damageType();
    [SerializeField] protected int damage;

    protected enum damageType
    {
        explosive,
        normals
    };
    protected List<Collider2D> explosiveDamage(float radius, Vector2 damagePosition, ContactFilter2D contactFilter, ref List<Collider2D> results)
    {
        int detected = Physics2D.OverlapCircle(damagePosition, radius, contactFilter, results);
        if (detected > 0)
            damageAOE(results);
        return results;
    }
    protected void damageAOE(List<Collider2D> results)
    {
        for (int i = 0; i <= results.Count - 1; i++)
        {
            DefaultEnemy enemyscript = results[i].GetComponentInParent<DefaultEnemy>();

            enemyscript.Damage(damage);
        }
    }
    protected void damageSingular(GameObject ThingToDamage, int Damage)
    {
        DefaultEnemy enemyscript = ThingToDamage.GetComponentInParent<DefaultEnemy>();
        enemyscript.Damage(Damage);
    }
    protected void rotateDefenceToTarget(string Tag, Transform target)
    {
        //if the defence has a target
        if (target.tag == Tag)
        {
            //keep rotating the defence in the direction of the target (the enemy)
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(Vector3.forward));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }
}
