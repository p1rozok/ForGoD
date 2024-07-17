using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, из которой будет вылетать пуля
    public float bulletSpeed = 10f; // Скорость пули

    public void Shoot()
    {
        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Добавляем скорость пуле
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }
}
