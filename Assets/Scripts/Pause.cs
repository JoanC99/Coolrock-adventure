using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject darkPanel; // Panel que oscurecer� la pantalla

    // Este m�todo se llamar� cuando se haga clic en el bot�n de pausa
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo del juego
        isPaused = true;
        darkPanel.SetActive(true); // Activa el panel oscuro
        Debug.Log("Juego pausado");

    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isPaused = false;
        darkPanel.SetActive(false); // Desactiva el panel oscuro
        Debug.Log("Juego reanudado");
    }
}