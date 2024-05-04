using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SingularDefence : DefenceDefault
{
    [SerializeField] protected DamageEffects Effects;

    [SerializeField] protected GameObject bullet;
    protected List<Transform> targets = new List<Transform>();

    [Header("Sound Settings")]
    [SerializeField] protected AudioClip[] turretFire;
    [SerializeField] protected AudioClip[] fireTrail;

    protected AudioClip turretFireAudio;
    protected AudioClip fireTrailAudio;

    protected AudioManager AM;
    protected AudioSource AS;

    protected void damageSingular(GameObject ThingToDamage, int Damage)
    {
        turretFireAudio = AM.getRandAudio(turretFire);
        fireTrailAudio = AM.getRandAudio(fireTrail);

        DefaultEnemy enemyscript = ThingToDamage.GetComponentInParent<DefaultEnemy>();
        enemyscript.Damage(Damage);

        if (typeOfEffect == effectType.None)
        {
            //float _time;
            //AS.PlayOneShot(turretFireAudio);
            AS.PlayOneShot(fireTrailAudio);
            //Debug.Log(fireTrailAudio);
            //AS.PlayOneShot
        }
        else if (typeOfEffect == effectType.Fire)
        {
            Effects.DEFire(enemyscript);
        }else if (typeOfEffect == effectType.Freeze)
        {
            ThingToDamage.GetComponent<EnemyDefault>().frozen = true;
            Debug.Log("freeze Damage");
            Effects.DEFreeze(enemyscript);
        }
    }
}
