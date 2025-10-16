using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner_6ix7even : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;

    public int initialMelee = 3;
    public int initialRanged = 2;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private int currentWave = 1;
    private int maxWaves = 3;

    private int meleeCount;
    private int rangedCount;

    void Start()
    {
        meleeCount = initialMelee;
        rangedCount = initialRanged;
        SpawnWave();
    }

    void Update()
    {
        spawnedEnemies.RemoveAll(e => e == null);

        if(spawnedEnemies.Count == 0 && currentWave < maxWaves)
        {
            currentWave++;
            meleeCount = Mathf.CeilToInt(meleeCount * 1.1f);
            rangedCount = Mathf.CeilToInt(rangedCount * 1.1f);
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < meleeCount; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-10f, 10), Random.Range(-10f, 10f), 0f);
            GameObject enemy = Instantiate(meleeEnemyPrefab, pos, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }

        for (int i = 0; i < rangedCount; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
            GameObject enemy = Instantiate(rangedEnemyPrefab, pos, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    public bool AllEnemiesDefeated()
    {
        spawnedEnemies.RemoveAll(e => e == null);
        return spawnedEnemies.Count == 0;
    }
}
