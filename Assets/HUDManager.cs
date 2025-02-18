using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<GameObject> vidas;
    private GameManager gameManager;

    [SerializeField] public Text itemsText;
    [SerializeField] private GameObject touch;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateItems();

        #if UNITY_ANDROID
            touch.SetActive(true);
        #else
            touch.SetActive(false);
        #endif
    }
    public void UpdateItems()
    {
        itemsText.text = "" + gameManager.items;
    }
    public void UpdateHUD()
    {
        if (gameManager.numVidas == 3)
        {
            vidas[0].SetActive(true);
            vidas[1].SetActive(true);
            vidas[2].SetActive(true);
        }
        else if(gameManager.numVidas == 2)
        {
            vidas[0].SetActive(true);
            vidas[1].SetActive(true);
            vidas[2].SetActive(false);
        }
        else if (gameManager.numVidas == 1)
        {
            vidas[0].SetActive(true);
            vidas[1].SetActive(false);
            vidas[2].SetActive(false);
        }
        else if (gameManager.numVidas == 0)
        {
            vidas[0].SetActive(false);
            vidas[1].SetActive(false);
            vidas[2].SetActive(false);
        }
    }
    public void ResetHUD()
    {
        vidas[0].SetActive(true);
        vidas[1].SetActive(true);
        vidas[2].SetActive(true);
    }
}
