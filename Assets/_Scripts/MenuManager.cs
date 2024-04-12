using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _roundCompletePanel;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
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
    }
}
