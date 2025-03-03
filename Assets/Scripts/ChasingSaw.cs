using System.Collections;
using UnityEngine;

public class ChasingSaw : MonoBehaviour
{
    public float speed = 3f; // Скорость движения пилы
    public float detectionRange = 5f; // Радиус обнаружения игрока
    public float knockbackForce = 11f;
    private Transform player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        transform.Rotate(0, 0, 200 * Time.deltaTime); // 200 - скорость вращения

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            // Двигаемся к игроку
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized; // Направление отталкивания
                other.gameObject.GetComponent<PlayerController>().getDamage(knockbackDirection , knockbackForce);
                playerHealth.takeLive();

            }
        }
    }
}