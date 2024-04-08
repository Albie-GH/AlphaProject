using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    [Header("Quota")] 
    [SerializeField] private GameObject _quotaPrefab;
    [SerializeField] private Transform _quotaParent;
    [SerializeField] private Sprite _emptyQuotaSprite;
    [SerializeField] private Sprite _fulfilledQuotaSprite;
    [SerializeField] private float _quotaSpacing = 100f;
    [SerializeField] private TMP_Text _quotaNeededText;
    [SerializeField] private TMP_Text _quotaReachedText;


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        _quotaNeededText.enabled = false;
        _quotaReachedText.enabled = false;
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        _coinsText.text = "Coins: " + StatsManager.Instance.ReturnTotalCoins();

        // Clear any existing quota dots
        foreach (Transform child in _quotaParent)
        {
            Destroy(child.gameObject);
        }

        float total_width = (StatsManager.Instance.ReturnTotalQuota() - 1) * _quotaSpacing;

        // Instantiate new quota dots
        for (int i = 0; i < StatsManager.Instance.ReturnTotalQuota(); i++)
        {
            float x_Pos = -total_width / 2 + i * _quotaSpacing;

            GameObject quota_GO = Instantiate(_quotaPrefab, _quotaParent);
            quota_GO.transform.localPosition = new Vector3(x_Pos, 0f, 0f);

            Image quota_image = quota_GO.GetComponent<Image>();
            if(i < StatsManager.Instance.ReturnCurrentQuota())
            {
                quota_image.sprite = _fulfilledQuotaSprite;
                Debug.Log("Fulfilled quota " + i);
            }
            else
            {
                quota_image.sprite = _emptyQuotaSprite;
                Debug.Log("Empty quota " + i);

            }
        }

        // Show quota reached text
        if(StatsManager.Instance.ReturnCurrentQuota() == StatsManager.Instance.ReturnTotalQuota())
        {
            _quotaReachedText.enabled = true;
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

}
