using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int TimeBetweenSpawns;
    [SerializeField] Transform spawnPosition;
    [SerializeField] private WaveScriptableObject[] EnemyTypes;

    //variables that don't need to be shown in the inspector
    private bool isWaiting = false;
    private bool _waitForWave = false;
    private int numWave = 0;

    [SerializeField] private Button continueToNext;
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private Points _points;

    private void Start()
    {
        _waveManager = GameObject.Find("----WaveManager----").GetComponent<WaveManager>();
        //waveManager =
    }
    private void Update()
    {
        //starts the coroutine everytime the coroutine ends, also decides if to spawn a wave or single enemy type
        if (isWaiting == false && !_waitForWave && numWave < EnemyTypes.Length)
        {
            StartCoroutine(_Spawner(_waveManager.CurrentWave));
        }
    }

    IEnumerator _Spawner(int currentWave)
    {
        _waveManager.CurrentWave += 1;
            for (int j = 0; j < EnemyTypes[currentWave].Enemies.Count; j++)
            {
                //spawns the enemeis in the scriptable objects
                Instantiate(EnemyTypes[currentWave].Enemies[j], spawnPosition.position, transform.rotation);
                //wait for set amount of seconds before spawning next enemy
                isWaiting = true;
                yield return new WaitForSeconds(TimeBetweenSpawns);
                isWaiting = false;
            }
        _points.nextRound = true;
        _waitForWave = true;
        continueToNext.gameObject.SetActive(true); 
        numWave++;
    }
    public void ContinueNextWave()
    {
        if(numWave < EnemyTypes.Length)
            _waitForWave = false;
    }
}
