using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float coinDestroyDuration = 2;
    [SerializeField] private float keyDestroyDuration = 2;
    private Door door = null;

    GameManager GameManager;
    HUDScript HUD;
    SoundManager SoundManager;

    public KeyCode useKeyKey = KeyCode.F;
    private string useKeyString;


    private void Awake()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        HUD = FindFirstObjectByType<HUDScript>();
        SoundManager = FindFirstObjectByType<SoundManager>();

        useKeyString = useKeyKey.ToString();
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
        // Exit
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
        // Keys
        else if (other.CompareTag("Key"))
        {
            StartCoroutine(PickupKey(other));
        }

        // Doors
        else if (other.CompareTag("Door"))
        {
            door = other.gameObject.GetComponent<Door>();
            if (door.isLocked)
            {
                HUD.ShowKeyText(StatsManager.Instance.keys, useKeyString);
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

    // Pick up coin
    IEnumerator PickupCoin(Collider coin)
    {
        //Debug.Log("Picked Up Coin");
        StatsManager.Instance.CollectCoin();
        HUD.UpdateUI();
        SoundManager.PlayClip(SoundManager.ClipType.CollectCoin);

        MeshRenderer[] meshRenderers =  coin.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }

        coin.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(coinDestroyDuration);

        Destroy(coin.gameObject);
    }

    // Pick up key
    IEnumerator PickupKey(Collider key)
    {
        //Debug.Log("Picked Up Key");
        StatsManager.Instance.CollectKey();
        HUD.UpdateUI();
        SoundManager.PlayClip(SoundManager.ClipType.CollectKey);


        key.GetComponent<MeshRenderer>().enabled = false;
        key.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(keyDestroyDuration);

        Destroy(key.gameObject);
    }

    private void MyInput()
    {
        if (door && Input.GetKeyDown(useKeyKey) && door.isLocked && StatsManager.Instance.keys > 0)
        {
            door.UnlockDoor();
            HUD.HideKeyText();
            HUD.UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.State == GameState.Play)
        {
            GameManager.UpdateGameState(GameState.Paused);
        }
    }
}
