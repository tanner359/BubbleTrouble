using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public enum Wave {Wave_1, Wave_2, Wave_3, Boss }

    public Wave wave;

    public List<GameObject> wave1Enemies;
    public List<GameObject> wave2Enemies;
    public List<GameObject> wave3Enemies;
    public GameObject BossEnemy;



    //public float minSpawnInterval;
    //public float maxSpawnInterval;

    //void Start()
    //{
    //    StartCoroutine(SpawnEnemy());
    //}


    //public IEnumerator SpawnEnemy()
    //{
    //    yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
    //    Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform.position, Quaternion.identity);
    //    StartCoroutine(SpawnEnemy());
    //}

    
}
