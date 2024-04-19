using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private string SceneName;

    private DefaultEnemy enemyScript = null;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //adds the enemy script on the collided object to the variable enemyScript
            //Debug.Log("triggered collider");
            enemyScript = collider.gameObject.GetComponent<DefaultEnemy>();


            //use the variable in enemyscript called damage to know the amount of damage to do to the finish
            Health -= enemyScript.damage;

            //assign the enemyscript to null to stop getting errors when the enemy is destroyed
            //Debug.Log("the health is" + Health);
            enemyScript = null;

            //destroy enemy
            Destroy(collider.gameObject.transform.parent.gameObject);

            //check if the finish lines health (the players health) is 0, if it is they die.
            if (Health <= 0)
            {
                Dead();
            }
        }
    }
    private void Dead()
    {
        //just loads the player into the main menu, scene name means that the scene it loads the player into can be changed in the inspector.
        SceneManager.LoadScene(SceneName);
    }
}
