using UnityEngine;

public class CorrienteAscendente : MonoBehaviour
{
    public float fuerzaAscenso = 5f; // Fuerza de la corriente ascendente

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el trigger tiene un Rigidbody2D
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplica una fuerza hacia arriba al Rigidbody2D del jugador
                rb.AddForce(Vector2.up * fuerzaAscenso, ForceMode2D.Force);
            }
        }
    }
}