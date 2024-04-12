using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private int _waveNumber = 0;

    [Header("Scenes")]
    public GameState state;

    public static event Action<GameState> OnStateChange;
    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
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