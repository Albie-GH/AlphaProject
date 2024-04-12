using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float coinDuration = 2;
    private Door door = null;

    GameManager GameManager;
    HUDScript HUD;

    public KeyCode useKeyKey = KeyCode.F;


    private void Awake()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        HUD = FindFirstObjectByType<HUDScript>();
    }

    private void Update()
    {
        MyInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Coins
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

        // Doors
        if (other.CompareTag("Door"))
        {
            door = other.gameObject.GetComponent<Door>();
            if (door.isLocked)
            {
                HUD.ShowKeyText(StatsManager.Instance.keys);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit
        if (other.CompareTag("Exit"))
        {
            HUD.HideQuotaNeededText();
        }

        // Doors
        if (other.CompareTag("Door"))
        {
            door = other.gameObject.GetComponent<Door>();
            if (door.isLocked)
            {
                HUD.HideKeyText();
            }
            door = null;
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

    private void MyInput()
    {
        if (door && Input.GetKeyDown(useKeyKey) && door.isLocked)
        {
            door.UnlockDoor();
            HUD.HideKeyText();
            HUD.UpdateUI();
        }
    }
}
