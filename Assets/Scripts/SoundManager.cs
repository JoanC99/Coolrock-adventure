using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool isFXEnabled = true;
    public bool isMusicEnabled = true;
    public float musicVolume = 1.0f; // Valor por defecto
    public float fxVolume = 1.0f; // Valor por defecto

    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    public AudioClip menuMusic;
    public AudioClip gameMusicLevel1;
    public AudioClip gameMusicLevel2;
    public AudioClip gameMusicLevel3;
    public AudioClip gameMusicLevel4;
    public AudioClip jumpFX;
    public AudioClip collectedFX;
    public AudioClip finishedFX;
    public AudioClip deadFX;
    public AudioClip clickFX;

    public static SoundManager Instance = null;

    void Awake()
    {
        // Implementar el patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el SoundManager entre escenas
        }
        else
        {
            Destroy(gameObject); // Asegurarse de que solo haya una instancia
        }
    }

    void Start()
    {
        // Establecer volúmenes iniciales
        SetMusicVolume(musicVolume);
        SetFXVolume(fxVolume);
        PlayMusic(menuMusic);
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        if (isFXEnabled)
        {
            EffectsSource.clip = clip;
            EffectsSource.Play();
        }
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        if (isMusicEnabled)
        {
            MusicSource.enabled = true;
            MusicSource.loop = true;
            MusicSource.clip = clip;
            MusicSource.Play();
        }
    }

    public void PlayFX(int i)
    {
        if (isFXEnabled)
        {
            EffectsSource.enabled = true;

            switch (i)
            {
                case 0:
                    EffectsSource.clip = jumpFX;
                    break;
                case 1:
                    EffectsSource.clip = collectedFX;
                    break;
                case 2:
                    EffectsSource.clip = finishedFX;
                    break;
                case 3:
                    EffectsSource.clip = deadFX;
                    break;
                case 4:
                    EffectsSource.clip = clickFX;
                    break;
            }
            EffectsSource.Play();
        }
    }

    public void PlayMusic(int i)
    {
        if (isMusicEnabled)
        {
            MusicSource.enabled = true;
            MusicSource.loop = true;
            switch (i)
            {
                case 0:
                    MusicSource.clip = menuMusic;
                    break;
                case 1:
                    MusicSource.clip = gameMusicLevel1;
                    break;
                case 2:
                    MusicSource.clip = gameMusicLevel2;
                    break;
                case 3:
                    MusicSource.clip = gameMusicLevel3;
                    break;
                case 4:
                    MusicSource.clip = gameMusicLevel4;
                    break;
            }
            MusicSource.Play();
        }
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    public void SetMusicVolume(float f)
    {
        musicVolume = f;
        MusicSource.volume = f;
    }

    public void SetFXVolume(float f)
    {
        fxVolume = f;
        EffectsSource.volume = f;
    }

    public void SetEnableFX(bool b)
    {
        isFXEnabled = b;
        if (b) PlayFX(0); // Reproduce un efecto de sonido si se habilitan los FX
    }

    public void SetEnableMusic(bool b)
    {
        isMusicEnabled = b;
        if (!b)
        {
            StopMusic();
        }
        else
        {
            // Reproduce la música del menú por defecto al habilitar la música
            PlayMusic(0);
        }
    }
}