using System.Collections;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    public float knockbackForce = 9.1f;  // Сила отталкивания
    public float timeUnderGround = 3f; // Время, которое шипы проводят под землёй
    public float timeAboveGround = 3f; // Время, которое шипы проводят на поверхности
    public float moveDistance = 1f; // Расстояние, на которое шипы будут опускаться под землю
    public float waitTime = 0f;
    private bool isActive = false; // Шипы активны (наносим урон)

    public bool isTriggered = false;
    public void Start()
    {
        if (!isTriggered)
        {
            StartCoroutine(WaitRoutine(waitTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Vector2 knockbackVector = (other.transform.position - transform.position).normalized;
                other.gameObject.GetComponent<PlayerController>().getDamage(knockbackVector, knockbackForce);
                playerHealth.takeLive();

            }
        }
    }

    private IEnumerator WaitRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(SpikeRoutine());
    }
    private IEnumerator SpikeRoutine()
    {
        while (true)
        {
            MoveSpikes(-moveDistance);
            isActive = false; 

            yield return new WaitForSeconds(timeUnderGround);

            MoveSpikes(moveDistance);
            isActive = true; 

            yield return new WaitForSeconds(timeAboveGround);
        }
    }
    
    public IEnumerator TriggeredSpikeRoutine()
    {
        while (true)
        {
            MoveSpikes(moveDistance);
            isActive = true; 

            yield return new WaitForSeconds(timeUnderGround);

            MoveSpikes(-moveDistance);
            isActive = false; 

            yield return new WaitForSeconds(timeAboveGround);
        }
    }

   
    
    private void MoveSpikes(float distance)
    {
        // Перемещаем шипы на заданное расстояние по оси Y
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y + distance);
        transform.position = newPosition;
    }
}
