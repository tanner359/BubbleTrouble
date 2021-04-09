using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Animator animator;
    public Transform spawnPoint;
    public GameObject bubblePrefab;
    public float spawnDelay = 4f;
    public float shootForce;
    public float bubbleLifeTime = 10;

    public static bool bubbleSpeedPwr = false;
    public static bool bubbleSpawnPwr = false;

    private void Awake()
    {
        StartCoroutine(SpawnDelay(spawnDelay));
    }

    public void SpawnBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, spawnPoint.position, new Quaternion(0,0,90,0));
        bubble.GetComponent<Rigidbody2D>().AddForce(-gameObject.transform.right * shootForce, ForceMode2D.Impulse);       
        Destroy(bubble, bubbleLifeTime);
    }

    public IEnumerator SpawnDelay(float spawnDelay) 
    {
        yield return new WaitForSeconds(spawnDelay);
        animator.SetTrigger("ShootBubble");
        if (bubbleSpawnPwr == true) spawnDelay = 1f; else spawnDelay = 4f;
        StartCoroutine(SpawnDelay(spawnDelay));
    }
}
