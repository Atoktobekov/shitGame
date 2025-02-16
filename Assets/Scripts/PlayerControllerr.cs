using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Range = UnityEngine.SocialPlatforms.Range;

public class PlayerControllerr : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool facingRight = true;
    private Animator animator;

    [Header("Player Movement Settings")] 
    [Range(5, 25f)] public float jumpForce;
    [Range(0, 10f)] public float speed;

    [Space] [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [Range(-5f,5f)] public float checkGroundOffSetY = -1.8f;
    [FormerlySerializedAs("checkGroundRadius")] [Range(0, 5f)] public float checkGroundRadius = 0.3f;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        horizontalMove = Input.GetAxis("Horizontal") * speed;
        if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }
        else if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
        }
    }
    private void FixedUpdate()
    {
        Move();
        checkGround();
        UpdateAnimator();
    }
    
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * 5f, rb.velocity.y);
        rb.velocity = targetVelocity;
    }
    private void UpdateAnimator()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);

        animator.SetBool("isJumping", !isGrounded); 
        animator.SetBool("isRunning", isGrounded && Mathf.Abs(rb.velocity.x) > 0.07f);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Jump()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);

        } 
    }

    private void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY), checkGroundRadius);

        if (colliders.Length > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
