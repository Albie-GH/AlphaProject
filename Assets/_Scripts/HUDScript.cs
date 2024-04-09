using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _levelText;

    [Header("Quota")] 
    [SerializeField] private GameObject _quotaPrefab;
    [SerializeField] private Transform _quotaParent;
    [SerializeField] private Sprite _emptyQuotaSprite;
    [SerializeField] private Sprite _fulfilledQuotaSprite;
    [SerializeField] private float _quotaSpacing = 100f;
    [SerializeField] private TMP_Text _quotaNeededText;
    [SerializeField] private TMP_Text _quotaText;

    [Header("Detection")]
    [SerializeField] private TMP_Text _detectingText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();

        _quotaNeededText.enabled = false;

        // UI that will remain for whole scene
        _levelText.text = _levelText.text + StatsManager.Instance.currentLevel.ToString();
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        _coinsText.text = "Coins: " + StatsManager.Instance.totalCoins;

        // Clear any existing quota dots
        foreach (Transform child in _quotaParent)
        {
            Destroy(child.gameObject);
        }

        float total_width = (StatsManager.Instance.totalQuota - 1) * _quotaSpacing;

        // Dynamically instantiate new quota dots
        for (int i = 0; i < StatsManager.Instance.totalQuota; i++)
        {
            float x_Pos = -total_width / 2 + i * _quotaSpacing;

            GameObject quota_GO = Instantiate(_quotaPrefab, _quotaParent);
            quota_GO.transform.localPosition = new Vector3(x_Pos, 0f, 0f);

            Image quota_image = quota_GO.GetComponent<Image>();
            if(i < StatsManager.Instance.quotaProgress)
            {
                quota_image.sprite = _fulfilledQuotaSprite;
                //Debug.Log("Fulfilled quota " + i);
            }
            else
            {
                quota_image.sprite = _emptyQuotaSprite;
                //Debug.Log("Empty quota " + i);

            }
        }

        Scene scene = SceneManager.GetActiveScene();
        
        // Show quota reached text
        if(StatsManager.Instance.quotaProgress >= StatsManager.Instance.totalQuota
            && scene.name != "Shop")
        {
            UpdateQuotaText("Quota reached! Proceed to exit.", Color.yellow);
        }
        else if (scene.name == "Shop")
        {
            UpdateQuotaText("Quota Next Round", Color.white);
            Debug.Log("Shop scene");
        }
    }

    // ***************
    // QUOTA INTERFACE
    // ***************
    public void ShowQuotaNeededText()
    {
        _quotaNeededText.enabled = true;
    }
    public void HideQuotaNeededText()
    {
        _quotaNeededText.enabled = false;
    }
    public void UpdateQuotaText(string text, Color color)
    {
        _quotaText.text = text;
        _quotaText.color = color;
    }

    // *******************
    // DETECTION INTERFACE
    // *******************
    public void ShowDetectedBar()
    {
        _detectingText.enabled = true;
    }
    public void HideDetectedBar()
    {
        _detectingText.enabled = false;
    }
}
