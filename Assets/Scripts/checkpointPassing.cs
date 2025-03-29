using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointPassing : MonoBehaviour
{
    private bool isPassed = false;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("Pass");
            if (!isPassed)
            {
                isPassed = true;
                collision.GetComponent<PlayerHealth>().SetCheckpoint(transform.position);
            }
        }
    }
}
