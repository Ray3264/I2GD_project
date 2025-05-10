using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeathCounter : MonoBehaviour
{
    [SerializeField] private int enemiesToKill = 10; // Количество врагов для победы
    private int killedEnemies = 0;

    // Подписываемся на событие при включении
    private void OnEnable()
    {
        Enemy.OnEnemyDeath += HandleEnemyDeath;
    }

    // Отписываемся при выключении
    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= HandleEnemyDeath;
    }

    // Обработчик смерти врага
    private void HandleEnemyDeath()
    {
        killedEnemies++;
        Debug.Log($"Врагов убито: {killedEnemies}/{enemiesToKill}");

        if (killedEnemies >= enemiesToKill)
        {
            LoadNextScene();
        }
    }

    // Загрузка следующей сцены
    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        // Проверяем, существует ли следующая сцена
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Это последняя сцена!");
            // Можно добавить переход в меню или финальную заставку
        }
    }
}