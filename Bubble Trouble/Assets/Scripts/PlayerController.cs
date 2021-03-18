using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    Vector3 touchPosition;
    public float movementSpeed;


    Vector3 tilt;
    Touch touch;
    private void FixedUpdate()
    {
        // tilt stuff  
        tilt = Input.acceleration;
        Debug.Log(tilt);
        Walk(tilt.x);
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began :
                    Swing();
                    break;
            }
        }
    }


    public void Swing()
    {
        if(gameObject.transform.position.x > 0)
        {
            animator.SetTrigger("SwingRight");
        }
        else if(gameObject.transform.position.x < 0)
        {
            animator.SetTrigger("SwingLeft");
        }
        else
        {
            animator.SetTrigger("SwingRight");
        }
    }

    public void Walk(float direction)
    {
        if (direction <= -0.25f)
        {
            animator.SetBool("Walk", true);
            rb.velocity = Vector2.left * movementSpeed;
        }
        else if (direction >= 0.25f)
        {
            animator.SetBool("Walk", true);
            rb.velocity = Vector2.right * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble") && attack)
        {
            collision.GetComponent<Bubble>().SetBubbleHit(true);
            Rigidbody2D bubbleRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = -(transform.position - bubbleRB.transform.position).normalized;
            bubbleRB.AddForce(direction * 50, ForceMode2D.Impulse);
        }
    }



    bool attack = false;
    public void EnableAttack()
    {
        attack = true;
    }

    public void DisableAttack()
    {
        attack = false;
    }


}
