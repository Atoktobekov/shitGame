using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject candyPrefab; // Префаб леденца
    public Transform firePoint;    // Точка, откуда вылетает леденец
    public float shootForce = 10f; // Сила выстрела
    public float fireRate = 0.5f;  // Задержка между выстрелами
    private float nextFireTime = 0f; // Время следующего выстрела
    private bool canShoot = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime && canShoot) // Стреляем 
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Устанавливаем задержку между выстрелами
        }
    }

    void Shoot()
    {
        GameObject candy = Instantiate(candyPrefab, firePoint.position, Quaternion.identity); // Создаём леденец
        Rigidbody2D rb = candy.GetComponent<Rigidbody2D>();

        // Определяем направление стрельбы (в зависимости от направления игрока)
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(direction * shootForce, 0f);
    }

    public void setCanShoot(bool val)
    {
        canShoot = val;
    }
}