using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MenuScript : MonoBehaviour
{
    StatsManager StatsManager;

    public void Start()
    {
        StatsManager = FindFirstObjectByType<StatsManager>();
    }
    public void StartGame()
    {
        if ( StatsManager != null )
        {
            StatsManager.ResetStatsManager();
        }
        SceneManager.LoadScene("PlayLevel");
    }
}
