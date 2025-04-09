using UnityEngine;
using System.Collections;
using TMPro;
using Cinemachine;

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
   public float cameraMoveDuration = 0.5f; // Время плавного возврата камеры
   
   public CinemachineVirtualCamera virtualCamera; // Cinemachine камера
   
      

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
         StartCoroutine(Die());
      }
   }

   public void plusLive()
   {
      lives++;
      livesText.text = lives.ToString();
   }

   public void plusCopy()
   {
      playerCopies++;
      playerCopiesText.text = playerCopies.ToString();
   }
   
  private IEnumerator Die()
  {
     AudioManager.instance.PlaySFX("die");
     player.SetCanMove(false); 
     isDead = true;
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
     AudioManager.instance.PlaySFX("die");
     player.SetCanMove(false); 
     isDead = true;
     anim.Play("Die");
     anim.SetTrigger("Die");
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
        AudioManager.instance.PlaySFX("checkpoint");
        respawnPoint = checkPoint;
   }
   
 
 private IEnumerator Respawn()
 {
    Vector3 oldPosition = transform.position; // Запоминаем старую позицию игрока
    transform.position = respawnPoint; // Устанавливаем игрока в точку респауна
        
    // Плавно двигаем и игрока, и камеру к точке респауна
    yield return StartCoroutine(SmoothCameraAndPlayerReset());

    anim.Play("Appearing"); // Запускаем анимацию появления
    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f); // Ждем окончания анимации

    isDead = false; // Игрок оживает
    player.SetCanMove(true); // Разрешаем движение игрока
 }
 
  private IEnumerator SmoothCameraAndPlayerReset()
  {
     Transform camTransform = virtualCamera.transform; // Получаем трансформ камеры
     Vector3 startPosition = camTransform.position; // Запоминаем начальную позицию камеры
     Vector3 targetPosition = new Vector3(respawnPoint.x, respawnPoint.y, camTransform.position.z); // Целевая позиция камеры
     float elapsedTime = 0f; // Время, прошедшее с начала интерполяции

     while (elapsedTime < cameraMoveDuration)
     {
        // Плавно двигаем и игрока, и камеру
        transform.position = Vector3.Lerp(startPosition, respawnPoint, elapsedTime / cameraMoveDuration); // Лерп для игрока
        camTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / cameraMoveDuration); // Лерп для камеры

        elapsedTime += Time.deltaTime; // Обновляем время
        yield return null; // Ждем следующего кадра
     }

     // Убеждаемся, что позиция игрока и камеры точно на месте
     transform.position = respawnPoint;
     camTransform.position = targetPosition;
  }
  
  private IEnumerator WaitForHitThenDie()
  {
     yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && 
                                      anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"));
     anim.SetTrigger("Die");
     anim.Play("Die"); 
  }

}
