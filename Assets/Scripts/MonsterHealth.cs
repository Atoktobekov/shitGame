using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public string collisiontag;
    public float bounceForce = 5f;
    private float knockbackForce = 9.35f;
    public int monsterLives = 1;
    public float positionYadding = 1f;
    private Animator anim;
    private bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisiontag && !isDead)
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            PlayerHealth playerHealth = coll.gameObject.GetComponent<PlayerHealth>();

            // Получаем мировые координаты позиции игрока и врага
            Vector2 playerPos = coll.transform.position;
            Vector2 monsterPos = transform.position;

            // Проверяем, находится ли игрок выше врага (в области головы)
            if (playerPos.y > monsterPos.y+positionYadding)
            {
                AudioManager.instance.PlaySFX("enemyExplosion");
                isDead = true;  // Помечаем, что враг умирает
                player.Bounce(bounceForce);
                StartCoroutine(DeathRoutine()); // Запускаем корутину смерти
            }
            else
            {
                    Vector2 knockbackDir = (transform.position.x > coll.transform.position.x) ? Vector2.left : Vector2.right;
                   player.getDamage(knockbackDir, knockbackForce);
                   playerHealth.takeLive();

            }
        }
    }
    
    private IEnumerator DeathRoutine()
    {
        anim.SetTrigger("Dead"); // Запускаем анимацию смерти

        // Ждём, пока анимация завершится
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject); // Удаляем объект после анимации
    }
}
