using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossDino : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 1f;

    [SerializeField] private GameObject projectilePrefab; // Prefab del proyectil
    [SerializeField] private Transform firePoint; // Punto desde donde se dispara el proyectil
    private float attackCooldown = 5f; // Tiempo entre ataques
    private float nextAttackTime = 0f; // Tiempo para el siguiente ataque

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    [System.Obsolete]
    void Update()
    {
        // Movimiento hacia el waypoint
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, this.transform.position) < 1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(waypoints[currentWaypointIndex].transform.position.x, transform.position.y), Time.deltaTime * speed);

        // Manejo del ataque
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown; // Reinicia el temporizador
        }
    }

    [System.Obsolete]
    void Attack()
    {
        anim.Play("DinoAtack"); // Activa la animación de ataque
        StartCoroutine(Shoot()); // Llama al método para disparar como una coroutine
    }

    [System.Obsolete]
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f); // Espera 0.5 segundos

        // Instancia el proyectil en la posición del firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Aquí puedes agregar lógica para que el proyectil se mueva hacia adelante
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {}
    }
}