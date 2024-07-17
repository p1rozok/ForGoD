
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider; // Ссылка на UI Slider для отображения здоровья

    void Start()
    {
        if (healthSlider == null)
        {
            Debug.LogError("Slider для здоровья не назначен!");
            return;
        }

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Логика смерти врага
        Debug.Log("Враг умер");
        Destroy(gameObject); // Удаляем объект врага
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthSlider.value = currentHealth;
    }
}
