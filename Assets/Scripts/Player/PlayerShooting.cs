using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, �� ������� ����� �������� ����
    public float bulletSpeed = 10f; // �������� ����

    public void Shoot()
    {
        // ������� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // ��������� �������� ����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }
}