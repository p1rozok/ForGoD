
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

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
        }
        else
        {
            Debug.LogError("������ Player � ����� 'Player' �� ������!");
        }
    }

    private void Update()
    {
        if (inventory != null)
        {
            if (transform.childCount <= 0)
            {
                inventory.isFull[i] = false;
            }
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Spawn spawnComponent = child.GetComponent<Spawn>();
            if (spawnComponent != null)
            {
                spawnComponent.SpawnDroppedItem();
            }
            Destroy(child.gameObject);
        }
    }
}
