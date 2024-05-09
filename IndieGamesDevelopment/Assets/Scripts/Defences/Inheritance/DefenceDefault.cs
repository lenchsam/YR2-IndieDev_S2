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

    [Header("Animations")]
    [SerializeField] protected Animator anim;

    protected GameObject currentAura;

    public enum effectType
    {
        None,
        Fire,
        Freeze
    };
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
