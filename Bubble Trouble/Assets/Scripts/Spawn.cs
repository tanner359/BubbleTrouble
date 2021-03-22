using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }


    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform.position, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }

    
}
