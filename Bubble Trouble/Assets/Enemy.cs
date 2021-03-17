using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector2 target;
    public float movementSpeed = 1;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            Destroy(gameObject);
        }
    }

    
}
