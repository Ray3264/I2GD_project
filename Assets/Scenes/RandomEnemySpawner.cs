using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] GameObject[] enemyPrefabs; // Массив префабов врагов
    [SerializeField] int maxEnemies = 5; // Максимальное количество врагов на сцене

    [Header("Spawn Zone Settings")]
    [SerializeField] Vector3 spawnZoneCenter = new Vector3(0, 0, 15);
    [SerializeField] Vector3 spawnZoneSize = new Vector3(20, 0, 10); // Размер зоны спавна (y=0 для плоской зоны)

    private int currentEnemies = 0;

    void Start()
    {
        // Первоначальный спавн врагов
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnRandomEnemy();
        }
    }

    void Update()
    {
        // Находим всех врагов на сцене
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    
        if (currentEnemies < maxEnemies)
        {
            SpawnRandomEnemy();
        }
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("No enemy prefabs assigned!");
            return;
        }

        // Выбираем случайный префаб
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Генерируем случайную позицию в зоне
        Vector3 randomPosition = new Vector3(
            spawnZoneCenter.x + Random.Range(-spawnZoneSize.x / 2, spawnZoneSize.x / 2),
            spawnZoneCenter.y,
            spawnZoneCenter.z + Random.Range(-spawnZoneSize.z / 2, spawnZoneSize.z / 2)
        );

        // Создаем врага
        GameObject enemy = Instantiate(randomPrefab, randomPosition, Quaternion.identity);
        
        // Случайный поворот
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);

        currentEnemies++;
    }

    void HandleEnemyDeath()
    {
        currentEnemies--;
    }

    // Визуализация зоны спавна в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(spawnZoneCenter, spawnZoneSize);
    }
}