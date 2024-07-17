
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            inventory = player.GetComponent<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("Компонент Inventory не найден на объекте Player!");
            }
            else
            {
                if (inventory.slots == null || inventory.slots.Length == 0)
                {
                    Debug.LogError("Массив slots в Inventory не инициализирован или пуст!");
                }
                if (inventory.isFull == null || inventory.isFull.Length == 0)
                {
                    Debug.LogError("Массив isFull в Inventory не инициализирован или пуст!");
                }
                else if (inventory.slots.Length != inventory.isFull.Length)
                {
                    Debug.LogError("Длины массивов slots и isFull не совпадают!");
                }
                else
                {
                    Debug.Log("Массивы slots и isFull успешно инициализированы.");
                }
            }
        }
        else
        {
            Debug.LogError("Объект Player с тегом 'Player' не найден!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inventory != null && inventory.slots != null && inventory.isFull != null)
        {
            if (inventory.slots.Length != inventory.isFull.Length)
            {
                Debug.LogError("Длины массивов slots и isFull не совпадают!");
                return;
            }

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i] == null)
                {
                    Debug.LogError($"Слот {i} не назначен в массиве slots!");
                    continue;
                }

                if (!inventory.isFull[i])
                {
                    // Создаем слот и удаляем объект из мира
                    Instantiate(slotButton, inventory.slots[i].transform);
                    inventory.isFull[i] = true;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
