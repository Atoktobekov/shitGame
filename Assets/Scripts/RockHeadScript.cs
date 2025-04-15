using System.Collections;
using UnityEngine;

public class RockHeadScript : MonoBehaviour
{
    public float knockbackForce = 9.5f;
    public int lives = 2;

    private Animator anim;
    public float blinkPeriod = 2f;
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(BlinkRoutine());
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

    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            anim.SetTrigger("Idle");
            yield return new WaitForSeconds(blinkPeriod);
            anim.SetBool("Blink", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("Blink", false);
        }
    }

    IEnumerator HitRoutine()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Hit", false);
    }

    IEnumerator DieRoutine()
    {
        AudioManager.instance.PlaySFX("enemyExplosion");
        anim.Play("RockHead_Death");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
    
}
