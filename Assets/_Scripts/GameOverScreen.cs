using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    void Start()
    {
        gameObject.SetActive(true);
        _levelText.text = "You reached level " + StatsManager.Instance.currentLevel.ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
