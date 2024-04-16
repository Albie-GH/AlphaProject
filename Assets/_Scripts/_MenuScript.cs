using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MenuScript : MonoBehaviour
{
    public void Awake()
    {
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        if ( StatsManager.Instance != null )
        {
            StatsManager.Instance.ResetStatsManager();
        }
        SceneManager.LoadScene("PlayLevel");
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
}
