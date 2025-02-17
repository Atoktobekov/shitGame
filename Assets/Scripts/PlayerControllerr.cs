
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerControllerr : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private float horizontalMove;
    private bool facingRight = true;
    private bool canMove = true;
    
    private Animator animator;
    
    public int extraJumps = 1;
    private int jumpCount = 0;

    [Header("Player Movement Settings")] 
    [Range(5, 25f)] public float jumpForce;
    [Range(0, 10f)] public float speed;

    [Space] [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [Range(-5f,5f)] public float checkGroundOffSetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Проверяем, что игрок касается платформы сверху
            if (collision.contacts[0].normal.y > 0)
            {
                transform.SetParent(collision.transform); // Делаем платформу родителем
            }
        }
    }
  //old function
    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null); // Убираем родителя при уходе с платформы
        }
    }*/
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (gameObject.activeInHierarchy) // Проверяем, активен ли объект перед сменой родителя
            {
                transform.SetParent(null);
            }
        }
    }

    private void Move()
    {
        if (!canMove) return;
        Vector2 targetVelocity = new Vector2(horizontalMove * 5f, rb.velocity.y);
        rb.velocity = targetVelocity;
    }
    private void UpdateAnimator()
    {
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isJumping", !isGrounded); 
        animator.SetBool("isGrounded", isGrounded); 
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
        if (isGrounded)
        {
            jumpCount = 0;
        }
        if ((isGrounded || jumpCount<extraJumps)
            && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Устанавливаем вертикальную скорость
            jumpCount++;
            animator.SetBool("isJumping", true);
        }

    }
    public void Bounce(float force)
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Сброс скорости по вертикали
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse); // Применяем импульс вверх
        }
    }

    public void gotDamageFromEnemy(Vector2 vec, float force)
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(vec.x * force, rb.velocity.y);
            StartCoroutine(KnockbackCoroutine(0.2f));

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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Цвет круга

        // Позиция проверки земли
        Vector2 groundCheckPos = new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY);

        // Рисуем круг, соответствующий checkGroundRadius
        Gizmos.DrawWireSphere(groundCheckPos, checkGroundRadius);
    }
    
    IEnumerator KnockbackCoroutine(float duration)
    {
        canMove = false; // Отключаем управление
        yield return new WaitForSeconds(duration);
        canMove = true; // Включаем обратно
    }

}
