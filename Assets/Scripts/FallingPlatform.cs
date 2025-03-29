using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f;  // Время перед падением
    public float resetDelay = 2f;  // Время перед возвратом
    public float returnDuration = 1f;  // Время анимации возврата
    private Vector3 originalPosition;  // Исходная позиция платформы
    private BoxCollider2D platformCollider;
    private Rigidbody2D rb;
    private SpriteRenderer[] partRenderers;  // Массив SpriteRenderer для частей платформы
    public float platformFallingGravityScale = 3.76f;
    private void Start()
    {
        originalPosition = transform.position;  // Сохраняем исходную позицию
        platformCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        
        rb.isKinematic = true;  // Убедимся, что платформа изначально кинематическая

        // Получаем SpriteRenderer для каждой части платформы
        partRenderers = GetComponentsInChildren<SpriteRenderer>();
        SetPartsAlpha(1f);  // Скрываем платформу в начале
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))  // Проверяем, что игрок наступил на платформу
        {
            // Проверяем, контактирует ли игрок сверху
            if (collision.contacts.Length > 0 && collision.contacts[0].point.y > transform.position.y)
            {
                Invoke("Fall", fallDelay);
            }
        }
    }

    private void Fall()
    {
        AudioManager.instance.PlaySFX("platformFall");
        platformCollider.enabled = false;  // Отключаем коллайдер, чтобы игрок не мог снова наступить на платформу
        rb.isKinematic = false;  // Делаем платформу подвижной
        rb.gravityScale = platformFallingGravityScale;  // Устанавливаем гравитацию, чтобы платформа падала

        Invoke("ResetPlatform", resetDelay);  // Запускаем возврат платформы
    }

    private void ResetPlatform()
    {
        transform.position = originalPosition;  // Возвращаем платформу на исходную позицию
        platformCollider.enabled = true;  // Включаем коллайдер снова
        rb.isKinematic = true;  // Делаем платформу снова кинематической
        rb.gravityScale = 0;  // Убираем влияние гравитации
        rb.velocity = Vector2.zero;  // Обнуляем скорость

        StartCoroutine(FadeIn());  // Запускаем корутину для проявления платформы
    }

    private IEnumerator FadeIn()
    {
        SetPartsAlpha(0f);  // Начинаем с полной прозрачности
        float elapsedTime = 0f;

        while (elapsedTime < returnDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / returnDuration);
            SetPartsAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;  // Ждем следующего кадра
        }

        SetPartsAlpha(1f);  // Убедимся, что платформы полностью видимы
    }

    private void SetPartsAlpha(float alpha)
    {
        foreach (var renderer in partRenderers)
        {
            Color color = renderer.color;
            color.a = alpha;  // Устанавливаем альфа-канал
            renderer.color = color;  // Применяем цвет к спрайту
        }
    }
}
