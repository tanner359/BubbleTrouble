using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : MonoBehaviour
{
    GameObject player;

    [Header("Attack #1")]
    public GameObject InkShotPrefab;
    public float shotSpeed = 1f;


    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void InkShot()
    {
        Vector2 direction = -(transform.position - player.transform.position).normalized;        
        GameObject shot = Instantiate(InkShotPrefab, transform.position, Quaternion.FromToRotation(-transform.right, direction));      
        shot.GetComponent<Rigidbody2D>().AddForce(direction * shotSpeed, ForceMode2D.Impulse);
        Destroy(shot, 8f);
    }
}
