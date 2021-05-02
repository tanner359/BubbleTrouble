using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Bubble : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;
    public bool wasHit = false;
    public AudioClip pop;
    public Sprite toxicBubble;
    public static GameObject explosion;
    SpriteRenderer sr;

    private void Awake()
    {
        if (Pipe.bubbleToxicPwr) { sr = GetComponentInChildren<SpriteRenderer>(); sr.sprite = toxicBubble; }
    }

    public bool WasBubbleHit(){return wasHit;}
    public void SetBubbleHit(bool state) { wasHit = state; }

    private void Start() { explosion = Resources.Load<GameObject>("Toxic Explosion"); }

    private void OnCollisionEnter2D(Collision2D collision)
    {                
        float yDiff = Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(collision.GetContact(0).point.y));
        float xDiff = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(collision.GetContact(0).point.x));

        float newAnimatorSpeed = (Mathf.Abs(rb.velocity.normalized.x) + Mathf.Abs(rb.velocity.normalized.y)) / 2.5f;

        if (yDiff > xDiff)
        {         
            animator.speed = newAnimatorSpeed;
            animator.SetTrigger("Squish_V");

        }
        else if (xDiff > yDiff)
        {
            animator.speed = newAnimatorSpeed;
            animator.SetTrigger("Squish_H");       
        }       
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float yDiff = Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(collision.transform.position.y));
        float xDiff = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(collision.transform.position.x));

        if (yDiff > xDiff)
        {
            animator.SetTrigger("Squish_V");
        }
        else if (xDiff > yDiff)
        {
            animator.SetTrigger("Squish_H");
        }
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(pop, transform.position, Settings.volume);
        if (Pipe.bubbleToxicPwr) Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
