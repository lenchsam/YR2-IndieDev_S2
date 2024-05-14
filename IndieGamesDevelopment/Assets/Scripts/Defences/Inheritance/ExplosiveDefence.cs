using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDefence : DefenceDefault
{
    protected DamageEffects Effects;
    [SerializeField] protected GameObject explosionEffect;
    protected List<Collider2D> DTExplosive(float radius, Vector2 damagePosition, ContactFilter2D contactFilter, ref List<Collider2D> results)
    {
        int detected = Physics2D.OverlapCircle(damagePosition, radius, contactFilter, results);
        //Debug.Log(detected);
        if (detected > 0)
            //Debug.Log(results[1].gameObject.name + " results are");
            damageAOE(results);
        return results;
    }
    protected void damageAOE(List<Collider2D> results)
    {
        for (int i = 0; i <= results.Count - 1; i++)
        {
            DefaultEnemy enemyscript = results[i].GetComponentInParent<DefaultEnemy>();

            enemyscript.Damage(damage);

            switch (typeOfEffect)
            {
                case effectType.None:
                    
                    //no custom logic
                    break;
                case effectType.Fire:
                    Effects.DEFire(enemyscript);
                    break;
                case effectType.Shadow:
                    Effects.DEFreeze(enemyscript);
                    break;
            }
        }
    }
}
