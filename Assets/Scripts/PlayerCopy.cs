using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
   private Animator anim;
   private bool isCollected;

   private void Start()
   {
      anim = GetComponent<Animator>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player") && !isCollected)
      {
         AudioManager.instance.PlaySFX("powerup");
         PlayerHealth health = other.GetComponent<PlayerHealth>();

         if (health != null)
         {
            health.plusCopy();
         }

         isCollected = true;

         StartCoroutine(DeathRoutine());
      }
   }
   
   private IEnumerator DeathRoutine()
   {
      anim.SetTrigger("Collected"); // Запускаем анимацию смерти

      // Ждём, пока анимация завершится
      yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

      Destroy(gameObject); // Удаляем объект после анимации
   }
}
