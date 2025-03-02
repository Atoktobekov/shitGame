using System;
using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{

    public float length;
    public float speed;

    private bool movingLeft = true;

    public Transform groundDetection;
    
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, length);
        
        if (!groundInfo.collider)
        {
            if (movingLeft)
            {
                transform.eulerAngles =  new Vector3(0, 180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
         
        Debug.DrawRay(groundDetection.position, Vector2.down * length, Color.red);

    }
}
