
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
                Debug.LogError("��������� Inventory �� ������ �� ������� Player!");
            }
            else
            {
                if (inventory.slots == null || inventory.slots.Length == 0)
                {
                    Debug.LogError("������ slots � Inventory �� ��������������� ��� ����!");
                }
                if (inventory.isFull == null || inventory.isFull.Length == 0)
                {
                    Debug.LogError("������ isFull � Inventory �� ��������������� ��� ����!");
                }
                else if (inventory.slots.Length != inventory.isFull.Length)
                {
                    Debug.LogError("����� �������� slots � isFull �� ���������!");
                }
                else
                {
                    Debug.Log("������� slots � isFull ������� ����������������.");
                }
            }
        }
        else
        {
            Debug.LogError("������ Player � ����� 'Player' �� ������!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inventory != null && inventory.slots != null && inventory.isFull != null)
        {
            if (inventory.slots.Length != inventory.isFull.Length)
            {
                Debug.LogError("����� �������� slots � isFull �� ���������!");
                return;
            }

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i] == null)
                {
                    Debug.LogError($"���� {i} �� �������� � ������� slots!");
                    continue;
                }

                if (!inventory.isFull[i])
                {
                    // ������� ���� � ������� ������ �� ����
                    Instantiate(slotButton, inventory.slots[i].transform);
                    inventory.isFull[i] = true;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
