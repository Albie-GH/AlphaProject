using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _coinsSpentText;
    [SerializeField] private TMP_Text _keysText;
    [SerializeField] private TMP_Text _keysUsedText;
    void Start()
    {
        gameObject.SetActive(true);
        _levelText.text = "You reached level " + StatsManager.Instance.currentLevel.ToString();
        _coinsText.text = StatsManager.Instance.coinsST.ToString();
        _coinsSpentText.text = StatsManager.Instance.coinsSpentST.ToString();
        _keysText.text = StatsManager.Instance.keysST.ToString();
        _keysUsedText.text = StatsManager.Instance.keysUsedST.ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
