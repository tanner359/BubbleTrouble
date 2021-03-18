using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float movementSpeed;


    Vector3 tilt;
    Touch touch;
    private void FixedUpdate()
    {
        // tilt stuff  
        tilt = Input.acceleration;
        Walk(tilt.x);
        
    }

    Vector2 touchPos;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Vector3 touchPoint = new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z);
            touchPos = Camera.main.ScreenToWorldPoint(touchPoint);
            switch (touch.phase)
            {
                default:
                    break;
                case TouchPhase.Began:
                    Swing();
                    break;
            }
        }
    }


    public void Swing()
    {
        if(touchPos.x >= 0)
        {
            animator.SetTrigger("SwingRight");
        }
        else if(touchPos.x < 0)
        {
            animator.SetTrigger("SwingLeft");
        }
    }

    public void Walk(float direction)
    {
        if (direction <= -0.10f)
        {
            animator.SetBool("Walk", true);
            rb.velocity = Vector2.left * movementSpeed * Mathf.Abs(direction);
        }
        else if (direction >= 0.10f)
        {
            animator.SetBool("Walk", true);
            rb.velocity = Vector2.right * movementSpeed * Mathf.Abs(direction);
        }
        else
        {
            animator.SetBool("Walk", false);
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
