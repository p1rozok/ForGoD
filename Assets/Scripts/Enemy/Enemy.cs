
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // �������� ������������ ����������
    public float detectionRange = 5f; // ������ ����������� ������
    public float attackRange = 1f; // ������ �����
    public int health = 10; // �������� ����������
    public int attackDamage = 10; // ���� �� �����
    public float attackCooldown = 2f; // �������� ����� �������
    public GameObject dropItem; // ������ ��������, ������� ����� ��������
    private Transform player; // ������ �� ������
    private Rigidbody2D rb;
    private float lastAttackTime;
    private Animator animator;
    public HealthBar healthBarPrefab;
    private HealthBar healthBar;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            Debug.LogError("Player �� ������!");
        }

        if (rb == null)
        {
            Debug.LogError("��� ���������� Rigidbody2D �� ������� " + gameObject.name);
        }

        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
            healthBar.SetMaxHealth(health);
        }
        else
        {
            Debug.LogError("������ ������� �������� �� ��������.");
        }

        lastAttackTime = Time.time - attackCooldown;
    }

    void Update()
    {
        if (player == null || rb == null) return;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player �� ������!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
        Debug.Log("������ �������� � ����������� ������. ��������: " + rb.velocity);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        // ������������ ����� � ����������� ��������
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void AttackPlayer()
    {
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("�����!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ������� ������� ��� ������ �����
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
