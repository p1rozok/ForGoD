using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ ����������
    public int numberOfEnemies = 3; // ���������� �����������
    public Vector2 spawnAreaMin; // ����������� ����� ������ (X, Y)
    public Vector2 spawnAreaMax; // ������������ ����� ������ (X, Y)

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
