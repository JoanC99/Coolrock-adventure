using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    // Este m�todo se llamar� cuando se haga clic en el bot�n de salir
    public void OnExitButtonClicked()
    {
        // Si estamos en el editor, simplemente detenemos la ejecuci�n
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si estamos en una construcci�n del juego, cerramos la aplicaci�n
            Application.Quit();
#endif
    }
}