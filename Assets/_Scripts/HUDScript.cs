using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _keysText;
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
    [SerializeField] private Image _detectionBar;
    [SerializeField] private Image _detectionFill;

    [Header("Doors")]
    [SerializeField] private TMP_Text _useKeyText;
    [SerializeField] private TMP_Text _needKeyText;


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();

        _quotaNeededText.enabled = false;
        _useKeyText.enabled = false;
        _needKeyText.enabled = false;

        // UI that will remain for whole scene
        _levelText.text = _levelText.text + StatsManager.Instance.currentLevel.ToString();
    }

    private void Update()
    {
        // Detection UI
        if (_detectionBar)
        {
            if(StatsManager.Instance.currentDetection <= 0)
            {
                _detectionFill.enabled = false;
                _detectionBar.enabled = false;
            }
            else
            {
                _detectionFill.enabled = true;
                _detectionBar.enabled = true;
            }
            _detectionFill.fillAmount = StatsManager.Instance.currentDetection / 100;
        }
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        _coinsText.text = "x" + StatsManager.Instance.totalCoins;
        _keysText.text = "x" + StatsManager.Instance.keys;

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
            UpdateQuotaText("Quota reached! Proceed to exit.", new Color32(252,210,74, 255));
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

    // *******************
    // DETECTION INTERFACE
    // *******************
    public void ShowKeyText(int keys, string keyButton)
    {
        _useKeyText.text = "Use Key\n[" + keyButton + "]";
        if(keys > 0)
        {
            _useKeyText.enabled = true;
        }
        else
        {
            _needKeyText.enabled = true;
        }
    }
    public void HideKeyText()
    {
        _useKeyText.enabled = false;
        _needKeyText.enabled = false;
    }

}
