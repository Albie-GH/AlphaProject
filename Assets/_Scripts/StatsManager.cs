using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Make singleton
    public static StatsManager Instance;

    private int _totalCoins = 0;
    private int _coinsThisRound = 0;
    private int _currentLevel = 1;
    private int _totalQuota = 2;
    private int _currentQuota = 0;

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
        _totalCoins++;
        _coinsThisRound++;
        _currentQuota++;
    }
    public int ReturnTotalCoins()
    {
        return _totalCoins;
    }
    public bool SpendCoins(int amount)
    {
        if(amount >= _totalCoins)
        {
            _totalCoins -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    // COINS THIS ROUND
    public int ReturnCoinsThisRound()
    {
        return _coinsThisRound;
    }
    public void ResetCoinsThisRound()
    {
        _coinsThisRound = 0;
    }

    // ***************
    // CURRENT LEVEL INTERFACE
    // ***************
    public int ReturnCurrentLevel()
    {
        return _currentLevel;
    }
    public void IncreaseCurrentLevel()
    {
        _currentLevel++;
    }

    // ***************
    // QUOTA INTERFACE
    // ***************
    public int ReturnTotalQuota()
    {
        return _totalQuota;
    }
    public int ReturnCurrentQuota()
    {
        return _currentQuota;
    }
}
