using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Scenes")]
    public GameState state;

    public int currentWave;

    public static event Action<GameState> OnStateChange;

    [SerializeField] private Button continueToNext;
    [SerializeField] private GameObject DefenceUI;
    [SerializeField] private EnemyCounterScriptableObject SO_EnemyCounter;
    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }
    private void Update()
    {
        if (SO_EnemyCounter.finishedSpawning && SO_EnemyCounter.numberOfEnemies == 0)
        {
            continueToNext.gameObject.SetActive(true);
            DefenceUI.SetActive(true);
        }
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                //custom logic for main menu
                break;
            case GameState.TowerDefence:
                //custom logic for TowerDefence
                break;
            case GameState.Farming:
                //custom logic for Farming
                break;
            case GameState.Lose:
                //custom logic for Lose Screen
                break;
        }
        //? is short for ifstatement
        //this sends an event if any script has subscribed to the event
        OnStateChange?.Invoke(newState);
    }
    public enum GameState
    {
    MainMenu,
    TowerDefence,
    Farming,
    Lose
    }
} 