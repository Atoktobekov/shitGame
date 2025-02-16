using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public string collisiontag;
    public float bounceForce = 5f;
    public BoxCollider2D headCollider;
    public Health monsterHealth;
    

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisiontag)
        {
            PlayerControllerr player = coll.gameObject.GetComponent<PlayerControllerr>();
            Health playerHealth = coll.gameObject.GetComponent<Health>();

            if (player != null)
            {
                if (headCollider.bounds.Contains(coll.transform.position))
                {
                    monsterHealth.takeLive();
                    player.Bounce(bounceForce);
                }
                else
                {
                    playerHealth.takeLive();
                }
            }
        }
        
    }
    
}
