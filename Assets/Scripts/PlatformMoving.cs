using UnityEngine;
public class PlatformMoving : MonoBehaviour
{
    public float speed = 2f; // Скорость платформы
    public Transform position1, position2; // Точки, между которыми двигается платформа

    private Vector3 pointA, pointB;
    private Vector3 nextPosition;

    private void Start()
    {
        // Преобразуем локальные координаты в глобальные
        pointA = position1.position;
        pointB = position2.position;
        nextPosition = pointA;
    }

    private void Update()
    {
        // Двигаем платформу к следующей точке
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        // Если достигли точки, меняем направление
        if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
        {
            nextPosition = (nextPosition == pointA) ? pointB : pointA;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Проверяем, что игрок стоит на платформе (контакт сверху)
            if (collision.contacts[0].normal.y < 0) 
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.activeInHierarchy) // Проверяем, активен ли игрок
            {
                collision.transform.SetParent(null);
            }
        }
    }
}

