using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
   public Slider slider;
   public Health playerHealth;

   private void Start()
   {
     // setMaxHealth(playerHealth.maxHealth);
   }

   private void Update()
   {
      //setHealth(playerHealth.health);
   }

   public void setMaxHealth(int health)
   {
      slider.maxValue = health;
      slider.value = health;
   }
   public void setHealth(int health)
   {
      slider.value = health;
   }
}
