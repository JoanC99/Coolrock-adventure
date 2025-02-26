using UnityEngine;

public class ProjectileManagerFireball : MonoBehaviour
{
    public float speed;    // Speed of the projectile
    public float lifetime; // Time before destruction

    private Rigidbody2D rb;
    private float dirX;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on the projectile.");
            return;
        }

        dirX = GameObject.Find("Player").GetComponent<PlayerManager>().facingDirection;

        // Set the initial velocity
        rb.linearVelocity = dirX * transform.right * speed;
        if (rb.linearVelocity.x < 0) // Asegúrate de comparar la componente x de la velocidad
        {
            rb.linearVelocity *= -1; // Multiplica la velocidad por -1 para invertirla
        }
        // Destroy the projectile after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

}
