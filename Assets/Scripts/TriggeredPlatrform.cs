using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredPlatrform : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что это игрок и он движется снизу
        if (other.CompareTag("Player") && other.transform.position.y - 1.7 < transform.position.y)
        {
            Debug.Log("Ignoring collision");
            // Игнорируем коллизию, позволяя игроку пройти через платформу снизу
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        // Когда игрок выходит, восстанавливаем коллизию
        if (other.CompareTag("Player"))
        {            
            Debug.Log("Ignoring to exit");
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), false);
        }
    }
}
