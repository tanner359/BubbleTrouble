using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum Type { Toxic, Speed, Spawn };
    public Type powerupType;
    SpriteRenderer sr;
    ParticleSystem ps;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position -= transform.up * Time.deltaTime * 5;
        if (transform.position.y < -99f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PowerupSystem.StartPowerup(powerupType));
            sr.enabled = false;
            ps.Clear();
            ps.Stop();
            //gameObject.SetActive(false);
        }
    }
}