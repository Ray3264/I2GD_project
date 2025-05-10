using UnityEngine;

public class HallucinationEffect : MonoBehaviour
{
    public GameObject shadowPrefab; // Теневая фигура
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (PlayerAnxiety.Instance.anxietyLevel > 70 && Time.time > nextSpawnTime)
        {
            SpawnShadow();
            nextSpawnTime = Time.time + Random.Range(5f, 15f);
        }
    }

    void SpawnShadow()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 10f;
        spawnPosition += new Vector3(Random.Range(-5f, 5f), 1, Random.Range(-5f, 5f));
        Instantiate(shadowPrefab, spawnPosition, Quaternion.identity);
    }
}