using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Transform spawnPoint; // Opcional: dónde aparece

    private float timer;
    void Start()
    {
        timer = spawnInterval;
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval; // reinicia el contador
        }
    }
    void SpawnEnemy()
    {
        Vector3 position = spawnPoint ? spawnPoint.position : transform.position;
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
