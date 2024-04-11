using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField]
    private bool basedOnAmountSpawned = false;
    [SerializeField]
    private int NumEnemyToSpawn = 5; //will be used to make waves of enemies determined by amount of enemies spawned
    [SerializeField]
    private int TimeBetweenSpawns;
    [SerializeField] Transform spawnPosition;

    [Space]

    [SerializeField] private GameObject[] EnemyTypes;

    //variables that don't need to be shown in the inspector
    private bool isWaiting = false;
    private bool spawnEnemies = true;
    private int EnemyCounter = 0;

    private void Update()
    {
        //starts the coroutine everytime the coroutine ends, also decides if to spawn a wave or single enemy type
        if (isWaiting == false && spawnEnemies == true)
        {
            StartCoroutine(_Spawner());
        }
    }

    IEnumerator _Spawner()
    {
        //gets a random number to spawn a random enemy type in the enemy list
        int randomNumber = Random.Range(0, EnemyTypes.Length - 1);

        Instantiate(EnemyTypes[randomNumber], spawnPosition.position, transform.rotation);

        //counts how many enemies have been spawned
        EnemyCounter++;

        //wait for set amount of seconds before spawning next enemy
        isWaiting = true;
        yield return new WaitForSeconds(TimeBetweenSpawns);
        isWaiting = false;

        //stops spawning enemies when a set amount of enemies have been spawned
        if (EnemyCounter >= NumEnemyToSpawn && basedOnAmountSpawned)
        {
            spawnEnemies = false;
        }
    }
}
