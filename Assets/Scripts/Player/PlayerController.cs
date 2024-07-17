using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // �������� ����������� ���������
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private bool facingRight = true;
    public Joystick joystick; // ������ �� ��������� ���������
    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        // ������������ �������� �� ��� Z
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // �������� �� null ��� ���������
        if (joystick != null)
        {
            // ������������� ��������� ��� ����������
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;

            // ���������� ���������
            if (animator != null)
            {
                bool isWalking = movement != Vector2.zero;
                animator.SetBool("isWalking", isWalking);

                // �������� ����������� �������� � ����� Flip ��� �������������
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
            UnityEngine.Debug.LogWarning("Joystick �� �������� � ����������");
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
