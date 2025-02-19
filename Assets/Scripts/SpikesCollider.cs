using System.Collections;
using UnityEngine;

public class SpikesCollider : MonoBehaviour
{
  [SerializeField] private MovingSpikes spikes;
  public float waitingTime = 1f;
  private bool isActive = false;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player" && !isActive)
    {
      isActive = true;
      StartCoroutine(TriggerSpikesRoutine());
    }
  }
  IEnumerator TriggerSpikesRoutine()
  {
    yield return StartCoroutine(waitingForActivate()); 
    spikes.StartCoroutine("TriggeredSpikeRoutine");
  }
  IEnumerator waitingForActivate()
  {
    yield return new WaitForSeconds(waitingTime);
  }
 
  
}
