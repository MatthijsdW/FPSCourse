using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float minSpawnX, maxSpawnX, minSpawnZ, maxSpawnZ;
    public float spawnDelay;
    public GameObject[] possibleEnemies;
    private float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnDelay)
        {
            lastSpawnTime = Time.time;
            GameObject enemy = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Length)]);
            enemy.transform.position = new Vector3(Random.Range(minSpawnX, maxSpawnX), 2, Random.Range(minSpawnZ, maxSpawnZ));
        }
    }
}
