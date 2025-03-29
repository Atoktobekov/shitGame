using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public string collisiontag;
    private Animator anim;
    private bool isCollected = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag(collisiontag) && !isCollected)
       {
           AudioManager.instance.PlaySFX("fruitPick");
           PlayerHealth health = other.GetComponent<PlayerHealth>();
           if (health != null)
           {
               health.plusLive();
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