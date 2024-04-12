using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int TimeBetweenSpawns;
    [SerializeField] Transform spawnPosition;
    [SerializeField] private WaveScriptableObject[] EnemyTypes;

    //variables that don't need to be shown in the inspector
    private bool isWaiting = false;

    private GameObject temp;
    [SerializeField] private WaveManager _waveManager;

    [SerializeField] private Points _points;

    private void Start()
    {
        temp = GameObject.Find("----WaveManager----");
        _waveManager = temp.GetComponent<WaveManager>();
        //waveManager =
    }
    private void Update()
    {
        //starts the coroutine everytime the coroutine ends, also decides if to spawn a wave or single enemy type
        if (isWaiting == false)
        {
            StartCoroutine(_Spawner());
        }
    }

    IEnumerator _Spawner()
    {
        for(int i = 0; i < EnemyTypes.Length; i++)
        {
            _waveManager._currentWave += 1;
            for (int j = 0; j < EnemyTypes[i].Enemies.Count; j++)
            {
                //spawns the enemeis in the scriptable objects
                Instantiate(EnemyTypes[i].Enemies[j], spawnPosition.position, transform.rotation);
                //wait for set amount of seconds before spawning next enemy
                isWaiting = true;
                yield return new WaitForSeconds(TimeBetweenSpawns);
                isWaiting = false;
            }
            _points.nextRound = true;
        }
        //Debug.Log("end of waves");
    }
}
