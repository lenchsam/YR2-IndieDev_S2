using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class DefenceDefault : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] protected float fireRate;
    public effectType typeOfEffect = new effectType();
    [SerializeField] protected int damage;

    [Header("VFX Settings")]
    [SerializeField] protected GameObject[] auras;

    protected GameObject currentAura;

    public enum effectType
    {
        None,
        Fire,
        Freeze
    };
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
    public void deleteAura()
    {
        if (currentAura != null)
            Destroy(currentAura);
    }
    public void instantiateAura(Collider2D defence)
    {
        if (typeOfEffect == effectType.None)
        {
            deleteAura();
        }
        //Debug.Log(gameObject.transform.parent.gameObject);
        if (typeOfEffect == effectType.Fire)
        {
            deleteAura();
            currentAura = Instantiate(auras[0], transform.position, transform.rotation);
        }
        else if (typeOfEffect == effectType.Freeze)
        {
            deleteAura();
            currentAura = Instantiate(auras[1], transform.position, transform.rotation);
        }
    }
}
