using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rb;
    public Animator animator;
    //public float movementSpeed;
    public ContactFilter2D incomingDamageFilter;
    List<Collider2D> incomingColliders = new List<Collider2D>();
    public enum Claw { Right, Left };

    public AudioSource hitsound;

    private void Awake()
    {
        instance = this;
    }

    bool Dead { get; set; }

    Vector3 tilt;
    Touch touch;
    private void FixedUpdate()
    {
        // tilt stuff  
        if (!Dead)
        {
            tilt = Input.acceleration;
            if(Input.GetJoystickNames().Length == 0)
            {
                Walk(tilt.x);
            }
            else
            {
                Walk(Input.GetAxis("Horizontal"));
            }
        }
    }

    Vector2 touchPos;
    private void Update()
    {
        #region Controls

        #region Touch
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
                    if (touchPos.x >= 0){Swing(Claw.Right);}
                    else if (touchPos.x < 0){Swing(Claw.Left);}
                    break;
            }
        }
        #endregion

        #region Controller
        if (Input.GetAxis("Attack Left") != 0)
        {
            Swing(Claw.Left);
        }
        if (Input.GetAxis("Attack Right") != 0)
        {
            Swing(Claw.Right);
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Launcher.instance.ToggleOptions();
        }

        #endregion
        #endregion

        if (Physics2D.OverlapCollider(GetComponent<BoxCollider2D>(), incomingDamageFilter, incomingColliders) > 0 && !Dead)
        {
            Launcher.instance.GameOver();
            Dead = true;
            animator.enabled = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.AddComponent<BoxCollider2D>();
                Rigidbody2D rb = transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                rb.AddForce(Vector2.one * 30f, ForceMode2D.Impulse);
            }
        }
    }
    bool attack = false;
    public void EnableAttack() { attack = true; }
    public void DisableAttack() { attack = false; }   
    public bool IsAttacking() { return attack; }
    public void Swing(Claw claw)
    {
        switch (claw)
        {
            case Claw.Left:
                animator.SetTrigger("SwingLeft");
                break;

            case Claw.Right:
                animator.SetTrigger("SwingRight");
                break;
        }      
    }
    public void Walk(float direction)
    {
        animator.SetFloat("MovementSpeed", Mathf.Abs(direction * 1.5f));
        if (direction <= -0.10f)
        {
            animator.SetBool("Walk", true);           
            rb.velocity = new Vector2(-1 * Settings.sensitivity * Mathf.Abs(direction), rb.velocity.y);
        }
        else if (direction >= 0.10f)
        {
            animator.SetBool("Walk", true);
            rb.velocity = new Vector2(1 * Settings.sensitivity * Mathf.Abs(direction), rb.velocity.y);
        }
        else
        {
            animator.SetBool("Walk", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public float hitForce = 50f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble") && attack)
        {
            collision.GetComponent<Bubble>().SetBubbleHit(true);
            Rigidbody2D bubbleRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = -(transform.position - bubbleRB.transform.position).normalized;
            bubbleRB.AddForce(direction * hitForce, ForceMode2D.Impulse);
            hitsound.pitch = Random.Range(0.65f, 1.0f);
            hitsound.volume = Settings.volume / 100;
            hitsound.Play();
        }
    }
}
