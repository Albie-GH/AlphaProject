using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Play);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.RoundComplete:
                HandleRoundComplete();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Shop:
                break;
            case GameState.FinalVictory:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void HandlePlay()
    {
        Time.timeScale = 1;
    }
    private void HandleLose()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void HandleRoundComplete()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}

public enum GameState
{
    Play,
    RoundComplete,
    Lose,
    Shop,
    FinalVictory
}