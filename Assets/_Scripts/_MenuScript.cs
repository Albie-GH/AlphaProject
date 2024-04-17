using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MenuScript : MonoBehaviour
{
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas shopCanvas;
    GameManager GameManager;
    SoundManager SoundManager;

    public void Awake()
    {
        Time.timeScale = 1;
        GameManager = FindFirstObjectByType<GameManager>();
        SoundManager = FindFirstObjectByType<SoundManager>();
    }
    public void StartGame()
    {
        if ( StatsManager.Instance != null )
        {
            StatsManager.Instance.ResetStatsManager();
        }
        SceneManager.LoadScene("PlayLevel");
    }
    void Update()
    {
        MyInput();
    }

    void MyInput()
    {
        // Pause menu 
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.State == GameState.Play)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "PlayLevel" || currentScene.name == "Shop")
            {
                GameManager.UpdateGameState(GameState.Paused);
                if(shopCanvas)
                    shopCanvas.enabled = false;
            }
        }
    }

    public void Shop()
    {
        StatsManager.Instance.ResetForNextRound();
        SceneManager.LoadScene("Shop");
    }

    public void NextRound()
    {
        SceneManager.LoadScene("PlayLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (mainMenuCanvas)
        {
            mainMenuCanvas.enabled = false;
        }
        GameManager.UpdateGameState(GameState.Settings);

    }

    public void BackToMainMenu()
    {
        mainMenuCanvas.enabled = true;
        GameManager.UpdateGameState(GameState.Play);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.UpdateGameState(GameState.Play);
    }

    public void PauseMenu()
    {
        GameManager.UpdateGameState(GameState.Paused);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToShop()
    {
        if(shopCanvas)
            shopCanvas.enabled = true;
        GameManager.UpdateGameState(GameState.Play);

    }
}
