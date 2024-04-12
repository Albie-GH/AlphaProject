using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MenuScript : MonoBehaviour
{
    public void Start()
    {

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
}
