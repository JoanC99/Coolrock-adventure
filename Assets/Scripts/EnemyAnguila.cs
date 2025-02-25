using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnguila : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Verificar si ha llegado al waypoint actual
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;

            // Resetear waypoints si llega al final
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

            // Ajustar la dirección de la Anguila
            Vector3 direction = (waypoints[currentWaypointIndex].transform.position - transform.position).normalized;
            transform.localScale = new Vector3(1, direction.y > 0 ? 1 : -1, 1);
        }

        // Mover la Anguila hacia el waypoint en diagonal (X e Y)
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
