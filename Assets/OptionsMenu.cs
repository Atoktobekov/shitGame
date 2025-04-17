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
        bool goingFullscreen = !Screen.fullScreen;

        if (goingFullscreen)
        {
            // Переключаем в полноэкранный режим с текущим разрешением монитора
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
        }
        else
        {
            // Переключаем в оконный режим 1280x720
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }

        UpdateFullscreenText();
    }


    void UpdateFullscreenText()
    {
        Debug.Log("Fullscreen mode changed");
        fullscreenText.text = (Screen.fullScreen ? "OFF" : "ON");
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