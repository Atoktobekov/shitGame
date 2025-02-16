
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
   public int lives = 1;
   public TMP_Text livesText;
   
   void Start()
   {
      livesText.text = lives.ToString();
   }
   public void takeLive()
   {
      lives--;
      livesText.text = lives.ToString();

      if (lives <= 0)
      {
         Destroy(gameObject);
      }
   }

   public void plusLive()
   {
      lives++;
      livesText.text = lives.ToString();

   }

  
   
}
