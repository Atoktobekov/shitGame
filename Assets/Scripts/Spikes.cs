using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float knockbackForce = 9.2f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект, с которым столкнулись, это игрок
        if (other.CompareTag("Player"))
        {
            // Получаем компонент Health у игрока (если он есть)
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.takeLive(); // Игрок получает урон
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized; // Направление отталкивания
               other.GetComponent<PlayerController>().getDamage(knockbackDirection , knockbackForce);
            }
        }
    }
}
