using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bigEnemyPrefab;
    public GameObject rollerPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);

        Instantiate(powerupPrefab, GenerateSpawnPoint(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            Instantiate(powerupPrefab, GenerateSpawnPoint(), powerupPrefab.transform.rotation);
            SpawnEnemyWave(waveNumber);
            SpawnBigEnemy(waveNumber);
            SpawnRoller(waveNumber);
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(spawnRange, -spawnRange);
        float spawnPosZ = Random.Range(spawnRange, -spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void SpawnEnemyWave(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPoint(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnBigEnemy(int waveNumber)
    {
        for (int i = 0; i < waveNumber - 5 ; i++)
        {
            Instantiate(bigEnemyPrefab, GenerateSpawnPoint(), bigEnemyPrefab.transform.rotation);
        }
    }

    void SpawnRoller(int waveNumber)
    {
        for (int i = 0; i < waveNumber - 10; i++)
        {
            Instantiate(rollerPrefab, GenerateSpawnPoint(), rollerPrefab.transform.rotation);
        }
    }
}
