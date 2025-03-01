using UnityEngine;
using System.Collections;
using TMPro;

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
      if (collision.CompareTag("Bottom"))
      {
         StartCoroutine(DieFromCringe());
      }   
   }
   public void takeLive()
   {
      if (isDead) return;
      lives--;
      livesText.text = lives.ToString();

      if (lives <= 0)
      {
         Debug.Log("Starting Die coroutine");
         StartCoroutine(Die());
      }
   }

   public void plusLive()
   {
      lives++;
      livesText.text = lives.ToString();
   }

  /* private IEnumerator Die()
   {
      player.SetCanMove(false);
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

   }*/
  private IEnumerator Die()
  {
     player.SetCanMove(false); 
     isDead = true;
     rb.velocity = Vector2.zero; 

     StartCoroutine(WaitForHitThenDie());

     yield return new WaitForSeconds(0.6f);
     
     playerCopies--; 
     if (playerCopies < 0)
     {
        Debug.Log("Game Over");
        Destroy(gameObject);
        yield break; 
     }
    
     playerCopiesText.text = playerCopies.ToString();
     lives = 3;
     livesText.text = lives.ToString();

     yield return StartCoroutine(Respawn()); 
  }

  private IEnumerator DieFromCringe()
  {
     player.SetCanMove(false); 
     isDead = true;
     rb.velocity = Vector2.zero; 
     
     anim.Play("Die");
     yield return new WaitForSeconds(0.6f);
     
     playerCopies--; 
     if (playerCopies < 0)
     {
        Debug.Log("Game Over");
        Destroy(gameObject);
        yield break; 
     }
    
     playerCopiesText.text = playerCopies.ToString();
     lives = 3;
     livesText.text = lives.ToString();

     yield return StartCoroutine(Respawn()); 
  }


   public void SetCheckpoint(Vector3 checkPoint)
   {
        respawnPoint = checkPoint;
   }
   
  private IEnumerator Respawn()
  {
     transform.position = respawnPoint; 
     rb.velocity = Vector2.zero;

     anim.Play("Appearing");
     yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

     isDead = false;
     player.SetCanMove(true);
  }
  
  private IEnumerator WaitForHitThenDie()
  {
     yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && 
                                      anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"));

     anim.Play("Die"); 
  }


}
