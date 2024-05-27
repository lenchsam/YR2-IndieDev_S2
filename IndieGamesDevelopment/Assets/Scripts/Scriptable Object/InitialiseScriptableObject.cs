using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseScriptableObject : MonoBehaviour
{
    [SerializeField] private WaveScriptableObject SO_pawnLocations;
    [SerializeField] private AmountOfPawnsScriptableObject SO_amountOfPawns;
    [SerializeField] private EnemyCounterScriptableObject SO_enemyCounter;
    [SerializeField] private TextOnScreenScriptableObjects SO_Text; 
    // Start is called before the first frame update
    void Start()
    {
        SO_pawnLocations.gameObjectList.Clear();
        SO_amountOfPawns.amountOfPawns = 0;
        SO_enemyCounter.numberOfEnemies = 0;
        SO_enemyCounter.finishedSpawning = true;
        SO_Text.screenText = "Wave 1 Starting";
    }
}
