using System.Collections;
using UnityEngine;

public class EndPress : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayAnimation());
        }
    }

    private IEnumerator PlayAnimation()
    {
        anim.SetTrigger("Press"); 
        yield return new WaitForSeconds(2.3f); 
        anim.SetTrigger("Idle"); 
    }
}