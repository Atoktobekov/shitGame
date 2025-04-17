using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_Text fullscreenText;
    public TMP_Text muteText;
    private bool isMuted = false;
    private float savedVolume = 1f;
    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.35f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        UpdateFullscreenText();
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.instance.SetSFXVolume(volume);
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        UpdateFullscreenText();
    }

    void UpdateFullscreenText()
    {
        Debug.Log("Fullscreen mode changed");
        fullscreenText.text = (Screen.fullScreen ? "ON" : "OFF");
    }
    
    
    public void ToggleMute()
    {
        if (isMuted)
        {
            AudioListener.volume = savedVolume;
        }
        else
        {
            savedVolume = AudioListener.volume;
            AudioListener.volume = 0f;
        }

        isMuted = !isMuted;
        muteText.text = (isMuted ? "ON" : "OFF");
    }
}