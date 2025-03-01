using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level4" && gameManager.items > 0)
        {
            ScoreManager.SaveMaxScore(gameManager.items);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
