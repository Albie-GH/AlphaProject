using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource src;
    [SerializeField] AudioClip sfx1, sfx2, sfx3;
    [SerializeField] AudioClip clipMusic;

    [SerializeField] Slider volumeSlider;

    void Start()
    {
        // Check for save
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1f);
        else
            Load();

        src.clip = clipMusic;
        src.loop = true;
        src.Play();
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
}
