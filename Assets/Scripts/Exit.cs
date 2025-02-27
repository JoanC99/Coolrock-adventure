using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    // Este método se llamará cuando se haga clic en el botón de salir
    public void OnExitButtonClicked()
    {
        // Si estamos en el editor, simplemente detenemos la ejecución
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si estamos en una construcción del juego, cerramos la aplicación
            Application.Quit();
#endif
    }
}