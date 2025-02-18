using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    public float knockbackForce = 9.1f;  // Сила отталкивания
    public float timeUnderGround = 3f; // Время, которое шипы проводят под землёй
    public float timeAboveGround = 3f; // Время, которое шипы проводят на поверхности
    public float moveDistance = 1f; // Расстояние, на которое шипы будут опускаться под землю

    private bool isActive = true; // Шипы активны (наносим урон)

    void Start()
    {
        StartCoroutine(SpikeRoutine());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Debug.Log("Player is Dead");
                playerHealth.takeLive();
                
                Vector2 knockbackVector = (other.transform.position - transform.position).normalized;
                
                other.gameObject.GetComponent<PlayerControllerr>().getDamageFromSpikes(knockbackVector, knockbackForce);
            }
        }
    }


    private IEnumerator SpikeRoutine()
    {
        while (true)
        {
            // Опускаем шипы под землю
            MoveSpikes(-moveDistance);
            isActive = false; // Отключаем урон
            Debug.Log("isActive = " + isActive);

            // Ждём, пока шипы будут под землёй
            yield return new WaitForSeconds(timeUnderGround);

            // Поднимаем шипы на поверхность
            MoveSpikes(moveDistance);
            isActive = true; // Включаем урон
            Debug.Log("Spike is " + isActive);

            // Ждём, пока шипы будут на поверхности
            yield return new WaitForSeconds(timeAboveGround);
        }
    }
    
    private void MoveSpikes(float distance)
    {
        // Перемещаем шипы на заданное расстояние по оси Y
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y + distance);
        transform.position = newPosition;
    }
}
