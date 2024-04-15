using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Make singleton
    public static StatsManager Instance;

    // Game variables
    [Header("Progress")]
    public int totalCoins = 0;
    public int coinsThisRound = 0;
    public int currentLevel = 1;
    public int quotaProgress = 0;

    [Header("Inventory")]
    public int keys = 0;

    // DIFFICULTY STATS
    [Header("Difficulty")]
    public int totalQuota = 2;
    public float enemyFOV = 90f;
    public float enemySpeed = 3.5f;
    public float enemyDetectTime = 100f;
    public float currentDetection = 0f;
    public float detectingSpeed = 100f;
    public float fastDetectingSpeed = 200f;
    public float undetectingSpeed = -25f;
    public float enemyDetectRange = 15f;
    public float enemyFastDetectRange = 5f;

    // Enemies
    private List<EnemyVision> _enemies = new List<EnemyVision>();


    private void Awake()
    {
        // Destroy any new instances
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetStatsManager()
    {
        Destroy(gameObject);
    }

    // ***************
    // COINS INTERFACE
    // ***************
    public void CollectCoin()
    {
        totalCoins++;
        coinsThisRound++;
        quotaProgress++;
    }
    public bool SpendCoins(int amount)
    {
        if(amount >= totalCoins)
        {
            totalCoins -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    // COINS THIS ROUND
    public void ResetCoinsThisRound()
    {
        coinsThisRound = 0;
    }

    // *************
    // KEY INTERFACE
    // *************
    public void CollectKey()
    {
        keys++;
    }


    // ***************
    // CURRENT LEVEL INTERFACE
    // ***************
    public void IncreaseCurrentLevel()
    {
        currentLevel++;
    }

    // ***************
    // QUOTA INTERFACE
    // ***************
    public void ResetQuotaProgress()
    {
        quotaProgress = 0;
    }

    // **************************
    // ENEMY DIFFICULTY INTERFACE
    // **************************


    // ***************************
    // CURRENT DETECTION INTERFACE
    // ***************************

    public void IncreaseCurrentDetection(float amount)
    {
        currentDetection += amount;
    }
    public bool AnyEnemyCanSeePlayer()
    {
        foreach(EnemyVision enemy in _enemies)
        {
            if (enemy.PlayerInSight())
            {
                return true;
            }
        }
        return false;
    }


    // ***************
    // ENEMY INTERFACE
    // ***************
    public void AddEnemy(EnemyVision enemy)
    {
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(EnemyVision enemy)
    {
        _enemies.Remove(enemy);
    }

    // ***************
    // NEXT ROUND RESET
    // ***************
    public void ResetForNextRound()
    {
        ResetCoinsThisRound();
        ResetQuotaProgress();
        IncreaseCurrentLevel();
    }
}
