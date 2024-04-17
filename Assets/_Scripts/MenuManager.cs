using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _roundCompletePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;

    void Awake()
    {
        // Subscribe to GameManagerOnGameStateChanged
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        // Unsubscribe to GameManagerOnGameStateChanged
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (_gameOverPanel)
        {
            _gameOverPanel.SetActive(state == GameState.Lose);
        }
        if (_roundCompletePanel)
        {
            _roundCompletePanel.SetActive(state == GameState.RoundComplete);
        }
        if (_pausePanel)
        {
            _pausePanel.SetActive(state == GameState.Paused);
        }
        if (_settingsPanel)
        {
            _settingsPanel.SetActive(state == GameState.Settings);
        }
    }
}
