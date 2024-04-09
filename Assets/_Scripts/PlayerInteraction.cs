using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float coinDuration = 2;

    GameManager GameManager;
    HUDScript HUD;

    private void Awake()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        HUD = FindFirstObjectByType<HUDScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            StartCoroutine(PickupCoin(other));
        }
        else if (other.CompareTag("Exit"))
        {
            if(StatsManager.Instance.quotaProgress >= StatsManager.Instance.totalQuota)
            {
                GameManager.UpdateGameState(GameState.RoundComplete);
            }
            else
            {
                HUD.ShowQuotaNeededText();
                Debug.Log("Need quota");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            HUD.HideQuotaNeededText();
        }
    }

    IEnumerator PickupCoin(Collider coin)
    {
        Debug.Log("Picked Up");
        StatsManager.Instance.CollectCoin();
        HUD.UpdateUI();

        coin.GetComponent<MeshRenderer>().enabled = false;
        coin.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(coinDuration);

        Destroy(coin.gameObject);
    }
}
