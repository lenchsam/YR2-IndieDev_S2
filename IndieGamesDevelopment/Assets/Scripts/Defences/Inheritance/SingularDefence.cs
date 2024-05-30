using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SingularDefence : DefenceDefault
{
    protected DamageEffects Effects;

    [Header("Misc")]
    [SerializeField] protected GameObject projectile;
    protected List<Transform> targets = new List<Transform>();

    protected AudioClip turretFireAudio;
    protected AudioClip fireTrailAudio;

    protected void damageSingular(GameObject ThingToDamage, int Damage)
    {
        turretFireAudio = AM.getRandAudio(fire);
        //fireTrailAudio = AM.getRandAudio(fire2);

        DefaultEnemy enemyscript = ThingToDamage.GetComponentInParent<DefaultEnemy>();
        enemyscript.Damage(Damage);

        if (typeOfEffect == effectType.None)
        {
            //float _time;
            AS.PlayOneShot(turretFireAudio);
            AS.volume = AM.GetComponent<AudioSource>().volume;
            //Debug.Log(fireTrailAudio);
            //AS.PlayOneShot
        }
        else if (typeOfEffect == effectType.Fire)
        {
            Effects.DEFire(enemyscript);
        }else if (typeOfEffect == effectType.Shadow)
        {
            //picks a position from the knightPositions array
            //instantiates a knight at that position
        }
    }
}
