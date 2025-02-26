using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform respawn;
    private GameObject player;

    // LEVEL STATES
    public bool isGameOver = false;
    public bool isLevelRestarted = false;
    public bool isLevelCompleted = false;
    public bool isGameCompleted = false;

    public int numVidas = 3;
    public int items = 0;

    void Start() {
        // SOUNDMANAGER
        InitSoundManager();
        player = GameObject.Find("Player");

        if (player == null) {
            Debug.LogError("Player not found in the scene!");
        }
    }

    void InitSoundManager() {
        SoundManager sndManager = SoundManager.Instance; // Acceso al Singleton
        if (sndManager != null) {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f); // Valor por defecto
            float fxVolume = PlayerPrefs.GetFloat("fxVolume", 1.0f); // Valor por defecto
            bool isMusicEnabled = PlayerPrefs.GetInt("music", 1) == 1; // Valor por defecto
            bool isFXEnabled = PlayerPrefs.GetInt("fx", 1) == 1; // Valor por defecto

            Debug.Log("Music Volume: " + musicVolume);
            Debug.Log("FX Volume: " + fxVolume);
            Debug.Log("Music Enabled: " + isMusicEnabled);
            Debug.Log("FX Enabled: " + isFXEnabled);

            sndManager.SetMusicVolume(musicVolume);
            sndManager.SetFXVolume(fxVolume);
            sndManager.SetEnableMusic(isMusicEnabled);
            sndManager.SetEnableFX(isFXEnabled);
            sndManager.PlayMusic(0);
        } else {
            Debug.LogError("SoundManager instance not found!");
        }
    }

    void Reset() {
        isGameOver = false;
        isLevelRestarted = false;
        isLevelCompleted = false;
        isGameCompleted = false;
    }

    void OnGUI() {
        if (isGameCompleted || isGameOver) {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 30;
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height / 2 - Screen.height / 8, Screen.width / 4, Screen.height / 4), isGameCompleted ? "CONGRATULATIONS!!" : "GAMEOVER!!", myButtonStyle)) {
                Reset();
                SceneManager.LoadScene(1);
            }
        }
    }

    public void GameOver() {
        isGameOver = true;
        Debug.Log("Game Over!");
        // Puedes descomentar la línea siguiente si deseas liberar restricciones del jugador
        // GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        SceneManager.LoadScene(1);
    }

    public void RestartLevel() {
        Debug.Log("Restarting Level");
        if (player != null) {
            player.transform.position = respawn.position;
            player.GetComponent<PlayerManager>().InitPlayer();
        } else {
            Debug.LogError("Player not found when trying to restart the level!");
        }
    }

    public void CompleteLevel() {
        if (SceneManager.GetActiveScene().name == "Level4") {
            isGameCompleted = true;
            Debug.Log("Level Completed!");
        } else {
            Debug.Log("Loading next level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}