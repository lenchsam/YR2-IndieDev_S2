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

    [Header("Construction")]
    [SerializeField] protected SpriteRenderer SRtoActivate;
    [SerializeField] protected GameObject GOtoActivate;
    [SerializeField] protected GameObject setInactive;

    [Header("Sound Settings")]
    [SerializeField] protected AudioClip[] fire;
    [SerializeField] protected AudioClip[] fire2;
    [SerializeField] protected AudioClip normalEffect;
    [SerializeField] protected AudioClip fireEffect;
    [SerializeField] protected AudioClip shadowEffect;

    [Header("Misc")]
    [SerializeField] protected bool warriorSpawned = false;
    protected AudioManager AM;
    protected AudioSource AS;
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
            AS.volume = AM.GetComponent<AudioSource>().volume;
            AS.PlayOneShot(normalEffect);
            //basic tower active
            effectTowers[0].SetActive(true);

            //turn all other towers off
            effectTowers[1].SetActive(false);
            effectTowers[2].SetActive(false);
        }
        //Debug.Log(gameObject.transform.parent.gameObject);
        if (typeOfEffect == effectType.Fire)
        {
            AS.volume = AM.GetComponent<AudioSource>().volume;
            AS.PlayOneShot(fireEffect);
            //fire tower active
            effectTowers[1].SetActive(true);

            //all other towers inactive
            effectTowers[0].SetActive(false);
            effectTowers[2].SetActive(false);
        }
        else if (typeOfEffect == effectType.Shadow)
        {
            AS.volume = AM.GetComponent<AudioSource>().volume;
            AS.PlayOneShot(shadowEffect);
            Debug.Log("changing to shadow");
            //Shadow tower active
            effectTowers[2].SetActive(true);

            //all other towers inactive
            effectTowers[0].SetActive(false);
            effectTowers[1].SetActive(false);
        }
    }
    protected void activateDefence()
    {
        //Debug.Log("LISTENED TO EVENTTTTTTT");
        setInactive.SetActive(false);
        SRtoActivate.enabled = true;
        GOtoActivate.SetActive(true);
    }
    
}
