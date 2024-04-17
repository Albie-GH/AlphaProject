using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource playerSource;
    [SerializeField] AudioClip clipMusic;
    [SerializeField] Slider volumeSlider;

    // Enum to represent different audio clips
    public enum ClipType
    {
        MenuButton,
        BuyButton,
        CollectCoin,
        CollectKey,
        UnlockDoor,
        GameOver,
        RoundComplete
    }

    // Dictionary to map enum values to audio clips
    private Dictionary<ClipType, AudioClip> clipDictionary = new Dictionary<ClipType, AudioClip>();

    void Start()
    {
        // Populate clip dictionary
        clipDictionary.Add(ClipType.MenuButton, menuButton);
        clipDictionary.Add(ClipType.BuyButton, buyButton);
        clipDictionary.Add(ClipType.CollectCoin, collectCoin);
        clipDictionary.Add(ClipType.CollectKey, collectKey);
        clipDictionary.Add(ClipType.UnlockDoor, unlockDoor);
        clipDictionary.Add(ClipType.GameOver, gameOver);
        clipDictionary.Add(ClipType.RoundComplete, roundComplete);

        // Check for save
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1f);
        else
            Load();

        // play and loop music
        if (musicSource)
        {
            musicSource.clip = clipMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void PlayClip(ClipType clipType)
    {
        if (playerSource && clipDictionary.ContainsKey(clipType))
        {
            playerSource.clip = clipDictionary[clipType];
            playerSource.Play();
        }
        else
        {
            Debug.LogWarning("Either AudioSource or AudioClip is not assigned.");
        }
    }

    // Audio clips kept private and assigned within the script
    [SerializeField] AudioClip menuButton;
    [SerializeField] AudioClip buyButton;
    [SerializeField] AudioClip collectCoin;
    [SerializeField] AudioClip collectKey;
    [SerializeField] AudioClip unlockDoor;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip roundComplete;
}
