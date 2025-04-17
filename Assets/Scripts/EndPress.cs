using System.Collections;
using UnityEngine;

public class EndPress : MonoBehaviour
{
    private Animator anim;
    public bool toSampleScene = false;
    public GameObject gemPicker;

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

            int collected = gemPicker.GetComponent<GemPicker>().getGems();
            int total = 99;
            
            FindObjectOfType<PauseMenu>().ShowFinish(collected, total);
            other.GetComponent<PlayerController>().SetCanMove(false);
        }
    }

    private IEnumerator PlayAnimation()
    {
        anim.SetTrigger("Press"); 
        yield return new WaitForSeconds(2.3f); 
        anim.SetTrigger("Idle");   
    }
}