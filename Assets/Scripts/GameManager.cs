using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private GameObject sndManager;
    [SerializeField] private Transform respawn;
    private GameObject player;
    //LEVEL STATES
    public bool isGameOver = false;
    public bool isLevelRestarted = false;
    public bool isLevelCompleted = false;
    public bool isGameCompleted = false;

    public int numVidas = 3;
    public int items = 0;

    void Start()  {
        //SOUNDMANAGER
        //InitSoundManager();
        player = GameObject.Find("Player");
    }

    void InitSoundManager() {
        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        sndManager.GetComponent<SoundManager>().SetMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        sndManager.GetComponent<SoundManager>().fxVolume = PlayerPrefs.GetFloat("fxVolume");
        sndManager.GetComponent<SoundManager>().SetEnableMusic(PlayerPrefs.GetInt("music")==1?true:false);
        sndManager.GetComponent<SoundManager>().isFXEnabled = PlayerPrefs.GetInt("fx")==1?true:false;
        sndManager.GetComponent<SoundManager>().PlayMusic(0);
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
            if ( GUI.Button(new Rect(Screen.width/2-Screen.width/8, Screen.height/2-Screen.height/8, Screen.width/4, Screen.height/4), isGameCompleted?"CONGRATULATIONS!!":"GAMEOVER!!", myButtonStyle)) {
                Reset();
                SceneManager.LoadScene(1);
            }
        }
    }

    public void GameOver() {
        isGameOver = true;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        SceneManager.LoadScene(1);
    }

    public void RestartLevel() {

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
        player.transform.position = respawn.position;
        player.GetComponent<PlayerManager>().InitPlayer();
    }

    public void CompleteLevel() {
        if (SceneManager.GetActiveScene().name == "Level4"){
            isGameCompleted = true;
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

