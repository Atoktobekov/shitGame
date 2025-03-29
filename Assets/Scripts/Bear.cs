using System.Collections;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public float knockbackForce = 9.5f;
    public int lives = 2;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            PlayerHealth playerHealth = coll.gameObject.GetComponent<PlayerHealth>();
            
            Vector2 knockbackDir = (transform.position.x > coll.transform.position.x) ? Vector2.left : Vector2.right;
            player.getDamage(knockbackDir, knockbackForce);
            playerHealth.takeLive();
        }

        if (coll.gameObject.CompareTag("Bullet"))
        {
            AudioManager.instance.PlaySFX("enemyDamage");
            lives--;
            Destroy(coll.gameObject);
            StartCoroutine(HitRoutine());
            if (lives <= 0)
            {
                StartCoroutine(DieRoutine());
            }
        }
    }

    IEnumerator HitRoutine()
    {
        anim.Play("Hit");
        yield return new WaitForSeconds(0.081f);
        if (lives > 0)
        {
            anim.SetTrigger("Idle");
        }
    }

    IEnumerator DieRoutine()
    {
        AudioManager.instance.PlaySFX("enemyExplosion");
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
    
}