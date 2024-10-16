using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Finish : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private Slider healthBar;

    [Header("DeathScreenSettings")]
    [SerializeField] private bool differentSceneOnDeath;
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject UIToActivate;
    [SerializeField] private EnemyCounterScriptableObject SO_EnemyCounter;
    [SerializeField] private TMP_Text wavetext;
    public int currentWave = 0;

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
            healthBar.value = Health / maxHealth;
            //assign the enemyscript to null to stop getting errors when the enemy is destroyed
            //Debug.Log("the health is" + Health);
            enemyScript = null;

            SO_EnemyCounter.numberOfEnemies--;//removes enemy from enemy counter when it reaches the finish point
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
        if (differentSceneOnDeath)
            SceneManager.LoadScene(SceneName);
        else
        {
            UIToActivate.SetActive(true);
            wavetext.text = "You Reached Wave: " + currentWave; 
        }
    }
}
