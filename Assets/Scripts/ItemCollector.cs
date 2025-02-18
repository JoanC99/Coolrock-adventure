using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private GameObject sndManager;
    private GameManager gameManager;
    private HUDManager hudManager;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();
        //sndManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Collectible")) 
        {
            gameManager.items++;
            hudManager.UpdateItems();
            col.gameObject.GetComponent<Animator>().SetTrigger("collected");
            //sndManager.GetComponent<SoundManager>().PlayFX(1);
            Destroy(col.gameObject);
        }
    }


}
