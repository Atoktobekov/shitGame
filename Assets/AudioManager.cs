using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("---------------Audio Sources---------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("---------------Audio Clips---------------")]
    public AudioClip backgroundMusic;
    
    [SerializeField] private Sound[] sounds; // Массив звуков с громкостью
    private Dictionary<string, Sound> soundDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Заполняем словарь
        soundDictionary = new Dictionary<string, Sound>();
        foreach (var sound in sounds)
        {
            soundDictionary[sound.name] = sound;
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    // 🎵 Воспроизведение музыки с возможностью смены трека
    public void PlayMusic(AudioClip music)
    {
        if (musicSource.clip == music) return; // Уже играет этот трек? Не переключаем

        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out Sound sound))
        {
            sfxSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning("Звук не найден: " + soundName);
        }
    }

    // 🎚 Изменение громкости
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // 🏗 Загрузка настроек громкости при запуске
    private void LoadSettings()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
}
