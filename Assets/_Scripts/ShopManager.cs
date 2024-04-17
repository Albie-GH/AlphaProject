using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public HUDScript HUD;
    SoundManager SoundManager;

    [Header("Keys1")]
    public TMP_Text key1CostText;
    public Button key1BTN;
    public int key1Cost = 2;

    [Header("Keys5")]
    public TMP_Text key5CostText;
    public Button key5BTN;
    public int key5Cost = 8;

    [Header("Jump")]
    public TMP_Text jumpCostText;
    public Button jumpBTN;
    public int jumpCost = 5;
    public TMP_Text jumpSoldOut;

    private void Awake()
    {
        SoundManager = FindFirstObjectByType<SoundManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateShop();
        key1BTN.onClick.AddListener(BuyKey1);
        key5BTN.onClick.AddListener(BuyKey5);
        jumpBTN.onClick.AddListener(BuyJump);
    }


    void UpdateShop()
    {
        // *****
        // 1 KEY
        key1CostText.text = key1Cost.ToString();

        if(key1Cost > StatsManager.Instance.totalCoins)
        {
            key1BTN.interactable = false;
            key1CostText.color = Color.red;
        }

        // ******
        // 5 KEYS
        key5CostText.text = key5Cost.ToString();

        if (key5Cost > StatsManager.Instance.totalCoins)
        {
            key5BTN.interactable = false;
            key5CostText.color = Color.red;
        }

        // ******
        // JUMP
        jumpCostText.text = jumpCost.ToString();
        jumpSoldOut.enabled = false;

        if (StatsManager.Instance.jumpUnlocked)
        {
            jumpBTN.gameObject.SetActive(false);
            jumpSoldOut.enabled = true;
        }
        
        else if (jumpCost > StatsManager.Instance.totalCoins)
        {
            jumpBTN.interactable = false;
            jumpCostText.color = Color.red;
        }



        // Update UI at end
        HUD.UpdateUI();
    }

    void BuyKey1()
    {
        // Buying 1 key
        if (StatsManager.Instance != null)
        {
            if (StatsManager.Instance.totalCoins >= key1Cost)
            {
                StatsManager.Instance.totalCoins -= key1Cost;
                StatsManager.Instance.keys += 1;
                SoundManager.PlayClip(SoundManager.ClipType.BuyButton);
            }
        }
        UpdateShop();
        
    }

    void BuyKey5()
    {
        // Buying 5 keys
        if (StatsManager.Instance != null)
        {
            if (StatsManager.Instance.totalCoins >= key5Cost)
            {
                StatsManager.Instance.totalCoins -= key5Cost;
                StatsManager.Instance.keys += 5;
                SoundManager.PlayClip(SoundManager.ClipType.BuyButton);


            }
        }
        UpdateShop();
    }

    void BuyJump()
    {
        // Buying jump
        if (StatsManager.Instance != null)
        {
            if (StatsManager.Instance.totalCoins >= jumpCost)
            {
                StatsManager.Instance.totalCoins -= jumpCost;
                StatsManager.Instance.jumpUnlocked = true;
                SoundManager.PlayClip(SoundManager.ClipType.BuyButton);

            }
        }
        UpdateShop();
    }
}
