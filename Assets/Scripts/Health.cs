using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Health : MonoBehaviour
{
   public int health;
   public int maxHealth;

   public void takeHit(int damage)
   {
      health -= damage;

      if (health < 0)
      {
         Destroy(gameObject);
      }  
   }

   public void setHealth(int bonusHealth)
   {
      health += bonusHealth;

      if (health > maxHealth)
      {
         health = maxHealth;
      }
   }
}
