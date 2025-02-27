using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    private SoundManager soundManager;
    public void loadScene(int i) {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlayFX(4);
        SceneManager.LoadScene(i);
        if (i == 1) { soundManager.PlayMusic(0); }
    }
}
