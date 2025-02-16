using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string collisiontag;
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisiontag)
        {
            Health health = coll.gameObject.GetComponent<Health>();
            health.plusLive();
            Destroy(gameObject);
        }
        
    }
    
}