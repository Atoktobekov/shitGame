using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{

   public int lives = 90;
   public TMP_Text livesText;
   
   private void Update()
   {
      livesText.text = lives.ToString();
   }
   public void takeLive()
   {
      lives--;
      if (lives <= 0)
      {
         Destroy(gameObject);
      }
   }

   public void plusLive()
   {
      lives++;
   }

  
   
}
