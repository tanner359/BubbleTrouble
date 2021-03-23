using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Enemy_Type { Ranged, Melee };

    Vector2 target;
    Rigidbody2D rb;
    SpriteRenderer[] renderers;

    [Space(10)]
    [Header("References")]
    public Transform body;

    [Space(10)]
    [Header("Stats")]
    public float movementSpeed = 1;
    public int Health = 1;

    [Space(10)]
    [Header("Movement Pattern")]
    public Enemy_Type enemyType;
    public List<Vector2> locations = new List<Vector2>();
    public List<float> speeds;
    public bool InTransit = false;


    public List<Vector2> GetLocations() { return locations; }
    public void SetLocation(Vector2 location, int index) { locations[index] = location; }

    private void Start()
    {       
        target = transform.position;
        renderers = body.GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(StartMove());
    }

    private void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }

    int i = 0;
    //private IEnumerator StartMove()
    //{      
    //    if(i < locations.Count)
    //    {
    //        InTransit = true;
    //        target = locations[i];
    //        movementSpeed = speeds[i];
    //        yield return new WaitUntil(() => (Vector2)transform.position == locations[i]);
    //        i++;
    //        StartCoroutine(StartMove());
    //    }
    //    else
    //    {
    //        i = 0;
    //        InTransit = false;
    //        yield return new WaitForSeconds(Random.Range(5, 10));
    //        StartCoroutine(StartMove());
    //    }      
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            Destroy(collision.gameObject);
            Health--;
            if(Health != 0)
            {
                for(int i = 0; i < renderers.Length; i++)
                {
                    StartCoroutine(DamageFlash(renderers[i], renderers[i].color, Color.red));
                }
            }
            else
            {               
                Destroy(gameObject);
            }            
        }
    }

    public IEnumerator DamageFlash(SpriteRenderer renderer, Color origin, Color change)
    {
        renderer.color = change;
        yield return new WaitForSeconds(0.1f);
        renderer.color = origin;
    }
}
