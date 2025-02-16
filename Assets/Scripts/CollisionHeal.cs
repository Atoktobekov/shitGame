using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public int collisionHeal = 10;

    public string collisiontag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == collisiontag)
        {
            Health health = coll.gameObject.GetComponent<Health>();
            health.setHealth(collisionHeal);
            Destroy(gameObject);

        }
        
    }
    
}