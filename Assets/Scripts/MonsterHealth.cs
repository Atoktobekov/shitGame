using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public string collisiontag;
    public float bounceForce = 5f;
    public BoxCollider2D headCollider;
    public int monsterLives = 1;
    public float positionYadding = 1f;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisiontag)
        {
            PlayerControllerr player = coll.gameObject.GetComponent<PlayerControllerr>();
            PlayerHealth playerHealth = coll.gameObject.GetComponent<PlayerHealth>();

            // Получаем мировые координаты позиции игрока и врага
            Vector2 playerPos = coll.transform.position;
            Vector2 monsterPos = transform.position;

            // Проверяем, находится ли игрок выше врага (в области головы)
            if (playerPos.y > monsterPos.y+positionYadding)
            {
                takeLive();
                player.Bounce(bounceForce);
            }
            else
            {
                    playerHealth.takeLive();
            }
        }
    }

    private void takeLive()
    {
        monsterLives--;
        if (monsterLives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
