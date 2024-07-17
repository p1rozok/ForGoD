
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        if (player != null)
        {
            // Позиция появления предмета относительно игрока (например, немного справа и ниже)
            Vector2 spawnPos = new Vector2(player.position.x + 1, player.position.y - 1);
            Instantiate(item, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Игрок не найден для спауна предмета.");
        }
    }
}
