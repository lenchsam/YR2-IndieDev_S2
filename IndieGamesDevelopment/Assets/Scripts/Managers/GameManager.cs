using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentWave;

    [SerializeField] private Button continueToNext;
    [SerializeField] private GameObject DefenceUI;
    [SerializeField] private EnemyCounterScriptableObject SO_EnemyCounter;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private int MaxRounds = 6;

   private void Update()
    {
        if (SO_EnemyCounter.finishedSpawning && SO_EnemyCounter.numberOfEnemies == 0)
        {
            continueToNext.gameObject.SetActive(true);
            DefenceUI.SetActive(true);
            if (currentWave >= MaxRounds)
            {
                victoryScreen.SetActive(true);
            }
        }
    }
} 