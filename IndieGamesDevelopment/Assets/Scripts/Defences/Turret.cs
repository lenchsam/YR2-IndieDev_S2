using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : SingularDefence
{
    private Transform primaryTarget;
    private float time;

    private Button continueToWaveButton; //to get the button click event in code

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Enemy")
            return;
        //this adds a new target to the list every time something enters the collider

        //Debug.Log("enemy entered collider");

        //primary target is always the next thing to enter the collider
        primaryTarget = collider.gameObject.transform;

        //Debug.Log(collision.gameObject.transform);
        targets.Add(collider.transform);
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("enemy exit collider");

        //if what left the collider was the primary target, reset primary target to null.
        if (collider.transform == primaryTarget)
            primaryTarget = null;

        //removes whatever left the collider from the list
        targets.Remove(collider.transform);
    }
    private void Start()
    {
        Effects = GameObject.Find("----DamageEffects----").GetComponent<DamageEffects>();
        
        AM = GameObject.Find("----AudioManager----").GetComponent<AudioManager>();
        AS = gameObject.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        continueToWaveButton = GameObject.Find("NextWave").GetComponent<Button>();
        continueToWaveButton.onClick.AddListener(() => activateDefence(gameObject.transform.parent.GetChild(0).gameObject)); //listen to button click.
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (primaryTarget == null)
            nextTarget();

        //rotates the defence to the target
        if (primaryTarget != null)
        {
            //Vector3 targetDir = primaryTarget.position - transform.position;
            //float angle = Vector2.Angle(targetDir, transform.position);
            float angle = Mathf.Rad2Deg * (Mathf.Atan2(primaryTarget.position.y - transform.position.y, primaryTarget.position.x - transform.position.x));
            anim.SetFloat("Angle", angle);
            anim.SetBool("isShooting", true);
            //rotateDefenceToTarget("Enemy", primaryTarget);
            //if a set amount of time has gone, it attacks the primary target
            if (time >= fireRate)
            {
                //fire bullet

                GameObject Bullet = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                Bullet.GetComponent<BulletSettings>().target = primaryTarget;
                
                BulletSettings bulletScript = Bullet.GetComponent<BulletSettings>();
                bulletScript.direction = primaryTarget.position;
                bulletScript.FireBullet();
                
                //damage the enemy
                damageSingular(primaryTarget.gameObject, damage);
                
                time = 0;
            }
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }
    private void nextTarget()
    {
        if (targets.Count != 0)
            primaryTarget = targets[0];
    }
    //private void activateDefence()
    //{
    //    //Debug.Log("LISTENED TO EVENTTTTTTT");
    //    setInactive.SetActive(false);
    //    SRtoActivate.enabled = true;
    //    GOtoActivate.SetActive(true);
    //}
}
