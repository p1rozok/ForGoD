using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения персонажа
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private bool facingRight = true;
    public Joystick joystick; // Ссылка на компонент джойстика
    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        // Стабилизация вращения по оси Z
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Проверка на null для джойстика
        if (joystick != null)
        {
            // Использование джойстика для управления
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;

            // Управление анимацией
            if (animator != null)
            {
                bool isWalking = movement != Vector2.zero;
                animator.SetBool("isWalking", isWalking);

                // Проверка направления движения и вызов Flip при необходимости
                if (movement.x > 0 && !facingRight)
                {
                    Flip();
                }
                else if (movement.x < 0 && facingRight)
                {
                    Flip();
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Joystick не назначен в инспекторе");
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
