using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
   private bool isDead = false;
   public int lives = 3;
   public int playerCopies = 3;
   public TMP_Text playerCopiesText;
   public TMP_Text livesText;
   private Vector3 respawnPoint;
   private PlayerController player;
   private Rigidbody2D rb;   
   private Animator anim;

   void Start()
   {
      player = GetComponent<PlayerController>();
      anim = GetComponent<Animator>();
      respawnPoint = transform.position;
      rb = GetComponent<Rigidbody2D>();
      livesText.text = lives.ToString();
      playerCopiesText.text = playerCopies.ToString();
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.tag == "Bottom")
      {
         StartCoroutine(Die());
      }   
   }
   public void takeLive()
   {
      if (isDead) return;
      lives--;
      livesText.text = lives.ToString();

      if (lives <= 0)
      {
         StartCoroutine(Die());
      }
   }

   public void plusLive()
   {
      lives++;
      livesText.text = lives.ToString();
   }

   private IEnumerator Die()
   {
      isDead = true;
      anim.SetTrigger("Die"); // Анимация смерти
      rb.velocity = Vector2.zero;  // Останавливаем движение

      player.SetCanMove(false);
      // Ожидаем завершения анимации смерти
      yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

      playerCopies--; // Уменьшаем количество копий игрока
      if (playerCopies < 0)
      {
         Debug.Log("Game Over");
         Destroy(gameObject); // Уничтожаем объект, если копии закончились
      }
      playerCopiesText.text = playerCopies.ToString();
      lives = 3;
      livesText.text = lives.ToString();
      StartCoroutine(Respawn()); // Восстановление игрока после смерти

   }

   public void SetCheckpoint(Vector3 checkPoint)
   {
        respawnPoint = checkPoint;
   }

  IEnumerator Respawn()
   {
      anim.SetTrigger("Appear"); // Анимация появления
      yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

      transform.position = respawnPoint; // Возвращаем игрока в точку респауна
      rb.velocity = Vector2.zero;  // Останавливаем движение
      isDead = false; // Игрок больше не мёртв
      
      
      player.SetCanMove(true);
   }
}
