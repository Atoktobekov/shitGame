using System.Collections;
using UnityEngine;

public class EndPress : MonoBehaviour
{
    private Animator anim;
    public bool toSampleScene = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            AudioManager.instance.PlaySFX("end");
            StartCoroutine(PlayAnimation());
            if (toSampleScene)
            {
                SceneController.instance.loadScene("SampleScene");
            }
            else
            {
                SceneController.instance.nextLevel();
            }
        }
    }

    private IEnumerator PlayAnimation()
    {
        anim.SetTrigger("Press"); 
        yield return new WaitForSeconds(2.3f); 
        anim.SetTrigger("Idle");   
    }
}