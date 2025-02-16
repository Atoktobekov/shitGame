using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHeal : MonoBehaviour
{
    public string collisiontag;
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisiontag)
        {
            PlayerHealth health = coll.gameObject.GetComponent<PlayerHealth>();
            health.plusLive();
            Destroy(gameObject);
        }
        
    }
    
}