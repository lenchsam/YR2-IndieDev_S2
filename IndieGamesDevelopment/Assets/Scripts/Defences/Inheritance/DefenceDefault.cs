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
    protected int currentEffect = 0; //used for taking points away from the player when they upgrade the defence
    protected int effectGoingTo; //used to compair the current effect to the effect the player upgraded to, means I can take the correct points away
    protected AudioManager AM;
    protected AudioSource AS;

    [Header("Points")]
    protected Points pointsScript;
    [SerializeField] protected int[] upgradeCosts;
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
            effectGoingTo = 0;
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
            effectGoingTo = 1;
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
            effectGoingTo = 2;
            AS.volume = AM.GetComponent<AudioSource>().volume;
            AS.PlayOneShot(shadowEffect);
            //Debug.Log("changing to shadow");
            //Shadow tower active
            effectTowers[2].SetActive(true);

            //all other towers inactive
            effectTowers[0].SetActive(false);
            effectTowers[1].SetActive(false);
        }
        //take points from player if they upgraded defence
        if (effectGoingTo != currentEffect && pointsScript.totalPoints >= upgradeCosts[effectGoingTo])
        {
            Debug.Log("take points away");
            currentEffect = effectGoingTo;
            pointsScript.totalPoints -= upgradeCosts[currentEffect];
            pointsScript.UpdatePointsText();
            //take points
        }
        else
        {
            Debug.Log("not taking any points away");
        }
    }
    protected void activateDefence(GameObject contructionGO)
    {
        if (!contructionGO.activeSelf)
            return;
        //Debug.Log("LISTENED TO EVENTTTTTTT");
        setInactive.SetActive(false);
        SRtoActivate.enabled = true;
        GOtoActivate.SetActive(true);
    }
    
}
