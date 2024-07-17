using System.Collections;

using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    public Joystick joystick;
    public Player player;  // ѕредположим, что у вас есть класс Player
    private float timeBtwShots;
    public float startTimeBtwShots;

    public int maxAmmo = 15; // ћаксимальное количество патронов
    private int currentAmmo;
    public float reloadTime = 2f; // ¬рем€ перезар€дки
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo; // ”станавливаем текущее количество патронов равным максимальному

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player не назначен и не найден по тегу.");
            }
        }

        if (joystick == null)
        {
            Debug.LogError("Joystick не назначен в инспекторе.");
        }

        if (shotPoint == null)
        {
            Debug.LogError("ShotPoint не назначен в инспекторе.");
        }

        if (bullet == null)
        {
            Debug.LogError("Bullet не назначен в инспекторе.");
        }
    }

    void Update()
    {
        if (player == null || joystick == null || shotPoint == null || bullet == null)
        {
            return;
        }

        if (isReloading)
        {
            return;
        }

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (player.controlType == Player.ControlType.PC)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if (player.controlType == Player.ControlType.Android)
        {
            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if ((Input.GetMouseButtonDown(0) || (joystick.Horizontal != 0 || joystick.Vertical != 0)) && currentAmmo > 0)
            {
                Shoot();
                timeBtwShots = startTimeBtwShots;
                currentAmmo--;
            }
            else if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("ѕерезар€дка...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("ѕерезар€дка завершена.");
    }
}
