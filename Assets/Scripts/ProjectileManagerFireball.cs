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

       if (dirX == -1){
            Vector3 currentScale = transform.localScale;
            currentScale.x = -currentScale.x;
            transform.localScale = currentScale;
        }

        // Set the initial velocity
        rb.linearVelocity = dirX * transform.right * speed; // Use 'right' for 2D movement

        // Destroy the projectile after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

}
