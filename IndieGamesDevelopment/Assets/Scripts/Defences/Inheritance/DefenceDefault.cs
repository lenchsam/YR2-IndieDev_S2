using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceDefault : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] protected float fireRate;
    public effectType typeOfEffect = new effectType();
    [SerializeField] protected int damage;

    [Header("VFX Settings")]
    [SerializeField] protected GameObject[] effectTowers;

    [Header("Animations")]
    [SerializeField] protected Animator anim;
    protected GameObject currentAura;

    public enum effectType
    {
        None,
        Fire,
        Shadow
    };
    public void instantiateAura(Collider2D defence)
    {
        if (typeOfEffect == effectType.None)
        {
            //basic tower active
            effectTowers[0].SetActive(true);

            //turn all other towers off
            effectTowers[1].SetActive(false);
            effectTowers[2].SetActive(false);
        }
        //Debug.Log(gameObject.transform.parent.gameObject);
        if (typeOfEffect == effectType.Fire)
        {
            //fire tower active
            effectTowers[1].SetActive(true);

            //all other towers inactive
            effectTowers[0].SetActive(false);
            effectTowers[2].SetActive(false);
        }
        else if (typeOfEffect == effectType.Shadow)
        {
            Debug.Log("changing to shadow");
            //Shadow tower active
            effectTowers[2].SetActive(true);

            //all other towers inactive
            effectTowers[0].SetActive(false);
            effectTowers[1].SetActive(false);
        }
    }
}
