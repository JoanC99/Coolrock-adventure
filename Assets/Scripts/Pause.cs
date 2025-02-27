using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject darkPanel; // Panel que oscurecerá la pantalla

    // Este método se llamará cuando se haga clic en el botón de pausa
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