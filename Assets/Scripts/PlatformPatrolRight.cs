using System;
using UnityEngine;

public class PlatformPatrolRight : MonoBehaviour
{

    public float length;
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;
    
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, length);
        
        if (!groundInfo.collider)
        {
            if (movingRight)
            {
                transform.eulerAngles =  new Vector3(0, 180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
         
        Debug.DrawRay(groundDetection.position, Vector2.down * length, Color.red);

    }
}