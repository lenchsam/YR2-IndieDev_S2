using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    //enemy goes into redius
    //knight starts attacking the enemy
    //disapears after a set time
    [Header("Base Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackSpeed;

    [Header("Animations")]
    [SerializeField] private Animator anim;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip[] swordSwing;
    private AudioManager AM;
    private AudioSource AS;
    private AudioClip swordSwingAudio;

    private float time;
    private List<Transform> targets = new List<Transform>();
    private Transform primaryTarget;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Enemy")
            return;
        //this adds a new target to the list every time something enters the collider

        //Debug.Log("enemy entered collider");

        //primary target is always the next thing to enter the collider
        if (primaryTarget == null)
            primaryTarget = collider.gameObject.transform;

        //Debug.Log(collision.gameObject.transform);
        targets.Add(collider.transform);

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("enemy exit collider");

        //removes whatever left the collider from the list
        targets.Remove(collider.transform);
        if (targets.Count == 0)
        {
            primaryTarget = null;
        }
        else
        {
            primaryTarget = targets[0];
        }
    }
    private void Start()
    {
        AM = GameObject.Find("----AudioManager----").GetComponent<AudioManager>();
        AS = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (primaryTarget != null)
        {
            //what angle is the enemy?
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(primaryTarget.position.y - transform.position.y, primaryTarget.position.x - transform.position.x));
            //Set the animation variables depending on what the angle of the enemy is
            if (time >= attackSpeed)
            {
                anim.SetFloat("Angle", angle);
                anim.SetBool("isAttacking", true);
                flipBool();

                //Debug.Log("attacking");
                damageEnemy(primaryTarget.gameObject,damage);
                time = 0;
            }
        }
    }
    private void flipBool()
    {
        if (anim.GetBool("isTurnOne"))
        {
            anim.SetBool("isTurnOne", false);
        }
        else
        {
            anim.SetBool("isTurnOne", true);
        }
    }
    private void damageEnemy(GameObject ThingToDamage, int Damage)
    {
        //play audio for sword swing
        swordSwingAudio = AM.getRandAudio(swordSwing);
        AS.volume = AM.GetComponent<AudioSource>().volume;
        AS.PlayOneShot(swordSwingAudio);

        DefaultEnemy enemyscript = ThingToDamage.GetComponentInParent<DefaultEnemy>();
        enemyscript.Damage(Damage);
    }
}
