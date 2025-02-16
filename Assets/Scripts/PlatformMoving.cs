using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public float speed = 2f; // Скорость платформы
    public Vector3 pointA; // Начальная точка
    public Vector3 pointB; // Конечная точка
    private bool movingToB = true;
    void Update()
    {
        // Двигаем платформу между точками A и B
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
            if (transform.position == pointB)
                movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            if (transform.position == pointA)
                movingToB = true;
        }
    }
}
