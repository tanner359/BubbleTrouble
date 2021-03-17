using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    Vector3 touchPosition;

    

    private void FixedUpdate()
    {
        if(moveDirection == 1)
        {
            rb.MovePosition(transform.position + Vector3.right);
        }
        else if(moveDirection == -1)
        {
            rb.MovePosition(transform.position + Vector3.left);
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

    int moveDirection = 0;
    public void MoveButtonDown(int direction)
    {
        if (direction == -1)
        {
            moveDirection = -1;
            animator.SetBool("Walk", true);
        }
        else if (direction == 1)
        {
            moveDirection = 1;
            animator.SetBool("Walk", true);
        }
    }

    public void MoveButtonUp()
    {
        moveDirection = 0;
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
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
