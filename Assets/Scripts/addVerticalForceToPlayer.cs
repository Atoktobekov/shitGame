using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addVerticalForceToPlayer : MonoBehaviour
{
    public float jumpForce = 10f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayHitAnimation());
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); 
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
            }
        }
    }
    
    private IEnumerator PlayHitAnimation()
    {
        anim.Play("ArrowUp_Hit");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); // Ждём окончания анимации
        anim.Play("ArrowUp");
    }
}
