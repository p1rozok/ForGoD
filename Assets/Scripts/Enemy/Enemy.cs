
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Скорость передвижения противника
    public float detectionRange = 5f; // Радиус обнаружения игрока
    public float attackRange = 1f; // Радиус атаки
    public int health = 10; // Здоровье противника
    public int attackDamage = 10; // Урон от атаки
    public float attackCooldown = 2f; // Задержка между атаками
    public GameObject dropItem; // Префаб предмета, который будет выпадать
    private Transform player; // Ссылка на игрока
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
            Debug.LogError("Player не найден!");
        }

        if (rb == null)
        {
            Debug.LogError("Нет компонента Rigidbody2D на объекте " + gameObject.name);
        }

        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
            healthBar.SetMaxHealth(health);
        }
        else
        {
            Debug.LogError("Префаб полоски здоровья не назначен.");
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
            Debug.LogError("Player не найден!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
        Debug.Log("Монстр движется в направлении игрока. Скорость: " + rb.velocity);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        // Поворачиваем врага в направлении движения
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
                Debug.Log("Атака!");
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
        // Создаем предмет при смерти врага
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
