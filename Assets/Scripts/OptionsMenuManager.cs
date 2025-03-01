using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    private SoundManager soundManager;
    public Scrollbar scBarMusic;
    public Scrollbar scBarFX;
    public Toggle togMusic;
    public Toggle togFX;

    void Start()
    {
        soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            // Establecer valores predeterminados si no existen en PlayerPrefs
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1.0f);
            }
            if (!PlayerPrefs.HasKey("fxVolume"))
            {
                PlayerPrefs.SetFloat("fxVolume", 1.0f);
            }

            // Obtener valores de PlayerPrefs
            float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
            float fxVolume = PlayerPrefs.GetFloat("fxVolume", 1.0f);
            bool isMusicEnabled = PlayerPrefs.GetInt("music", 1) == 1;
            bool isFXEnabled = PlayerPrefs.GetInt("fx", 1) == 1;

            // Asignar valores a los componentes de UI
            togMusic.isOn = isMusicEnabled;
            togFX.isOn = isFXEnabled;
            scBarMusic.value = musicVolume;
            scBarFX.value = fxVolume;

            // Asignar valores al SoundManager
            soundManager.SetMusicVolume(musicVolume);
            soundManager.SetFXVolume(fxVolume);
            soundManager.SetEnableMusic(isMusicEnabled);
            soundManager.SetEnableFX(isFXEnabled);

            scBarFX.onValueChanged.AddListener(ScrollbarCallBack);
        }
        else
        {
            Debug.LogError("SoundManager instance not found!");
        }
    }

    void ScrollbarCallBack(float f)
    {
        if (f > 0)
            soundManager.PlayFX(0); // Usar el índice correcto para el efecto de sonido
    }

    public void SetVolumeMusic()
    {
        if (soundManager != null)
        {
            soundManager.SetMusicVolume(scBarMusic.value);
            PlayerPrefs.SetFloat("musicVolume", scBarMusic.value);
        }
    }

    public void SetVolumeFX()
    {
        if (soundManager != null)
        {
            soundManager.SetFXVolume(scBarFX.value);
            PlayerPrefs.SetFloat("fxVolume", scBarFX.value);
        }
    }

    public void EnableMusic()
    {
        if (soundManager != null)
        {
            soundManager.SetEnableMusic(togMusic.isOn);
            PlayerPrefs.SetInt("music", togMusic.isOn ? 1 : 0);
        }
    }

    public void EnableFX()
    {
        if (soundManager != null)
        {
            soundManager.SetEnableFX(togFX.isOn);
            PlayerPrefs.SetInt("fx", togFX.isOn ? 1 : 0);
        }
    }
}