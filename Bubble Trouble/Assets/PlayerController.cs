using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    Vector3 touchPosition;

    void Update()
    {

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Mathf.Abs(Camera.main.transform.position.z)));


        //    switch (touch.phase)
        //    {
        //        case TouchPhase.Stationary:
        //            if (touchPosition.x > 0)
        //            {
        //                rb.MovePosition(transform.position + Vector3.right);
        //                animator.SetBool("Walk", true);
        //            }
        //            else if (touchPosition.x < 0)
        //            {
        //                rb.MovePosition(transform.position + Vector3.left);
        //                animator.SetBool("Walk", true);
        //            }
        //            else
        //            {
        //                animator.SetBool("Walk", false);
        //            }
        //            break;
        //    }
        //}
    }

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


}
