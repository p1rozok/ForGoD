using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Цель, за которой будет следовать камера (персонаж)
    public float smoothing = 5f; // Скорость сглаживания движения камеры
    public Vector3 offset; // Смещение камеры относительно цели

    void Start()
    {
        // Рассчитываем начальное смещение от цели
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Желаемая позиция камеры
        Vector3 targetCamPos = target.position + offset;

        // Плавное перемещение камеры к желаемой позиции
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
