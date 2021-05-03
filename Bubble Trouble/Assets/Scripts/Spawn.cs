using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    public enum Wave {Wave_1, Wave_2, Wave_3, Boss }
        
    //public Wave wave;

    public float W1SpawnInterval;
    public float W2SpawnInterval;
    public float W3SpawnInterval;

    public List<EnemyNode> wave1Enemies = new List<EnemyNode>();
    public List<EnemyNode> wave2Enemies = new List<EnemyNode>();
    public List<EnemyNode> wave3Enemies = new List<EnemyNode>();
    public EnemyNode Boss;

    public int numEnemiesSpawned = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }


    public void StartWave(Wave wave)
    {
        StartCoroutine(SpawnEnemy(wave));
    }

    public void Update()
    {
        numEnemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public IEnumerator SpawnEnemy(Wave wave)
    {
        switch (wave)
        {
            case Wave.Wave_1:
                for(int i = 0; i < wave1Enemies.Count; i++)
                {
                    Instantiate(wave1Enemies[i].gameObject, wave1Enemies[i].spawnPoint.position, Quaternion.identity);                
                    yield return new WaitForSeconds(W1SpawnInterval);                   
                }
                GameManager.instance.NextWave();
                break;

            case Wave.Wave_2:
                for (int i = 0; i < wave2Enemies.Count; i++)
                {
                    Instantiate(wave2Enemies[i].gameObject, wave2Enemies[i].spawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(W2SpawnInterval);
                }
                GameManager.instance.NextWave();
                break;

            case Wave.Wave_3:
                for (int i = 0; i < wave3Enemies.Count; i++)
                {
                    Instantiate(wave3Enemies[i].gameObject, wave3Enemies[i].spawnPoint.position, Quaternion.identity);
                    yield return new WaitForSeconds(W3SpawnInterval);
                }
                GameManager.instance.NextWave();
                break;

            case Wave.Boss:
                Instantiate(Boss.gameObject, Boss.spawnPoint.position, Quaternion.identity);
                break;
        }
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }
}

[System.Serializable]
public struct EnemyNode
{
    public GameObject gameObject;
    public Transform spawnPoint;

    public EnemyNode(GameObject enemy, Transform spawnPoint)
    {
        this.gameObject = enemy;
        this.spawnPoint = spawnPoint;
    }
}

