using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    public float knockbackForce = 9.22f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
           if (playerHealth != null)
           {
               Vector2 knockbackDirection = (other.transform.position - transform.position).normalized; // Направление отталкивания
               other.gameObject.GetComponent<PlayerController>().getDamage(knockbackDirection , knockbackForce);
               playerHealth.takeLive(); // Игрок получает урон

           }
        }
    }
    
    
}
