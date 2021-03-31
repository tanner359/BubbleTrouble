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
   
    [Space(5)]
    [Header("Boundry Modifiers")]
    public float MIN_X = 0;
    public float MAX_X = 0;
    public float MIN_Y = 0;
    public float MAX_Y = 0;
   
    Vector2 Bottom_L;
    Vector2 Top_L;
    Vector2 Bottom_R;
    Vector2 Top_R;
    //public float rangeFromPlayer;

    [Space(10)]
    [Header("Movement Pattern")]
    public Enemy_Type enemyType;
    [SerializeField] public List<Vector2> locations;
    public List<float> speeds;
    public bool attack = false;
    public bool wander = false;
    public bool chase = false;

    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderers = body.GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Chase(GeneratePointInBounds()));        
    }

    public void UpdateValues()
    {
        //updates the values in the editor
    }

    public void Update()
    {
        //Update location values in editor
        //rangeFromPlayer = GetDistanceFromPlayer();
        Bottom_L = Camera.main.ScreenToWorldPoint(new Vector3(0 + MIN_X, 0 + MIN_Y, Camera.main.transform.position.z));
        Top_L = Camera.main.ScreenToWorldPoint(new Vector3(0 + MIN_X, Screen.height + MAX_Y, Camera.main.transform.position.z));
        Bottom_R = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + MAX_X, 0 + MIN_Y, Camera.main.transform.position.z));
        Top_R = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + MAX_X, Screen.height + MAX_Y, Camera.main.transform.position.z));

        Debug.DrawLine(Bottom_L, Bottom_R, Color.red);
        Debug.DrawLine(Bottom_R, Top_R, Color.red);
        Debug.DrawLine(Top_R, Top_L, Color.red);
        Debug.DrawLine(Bottom_L, Top_L, Color.red); 
    }

    


    private void FixedUpdate()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (attack || wander || chase)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
        } 
    }

    int i = 0;
    private IEnumerator Attack()
    {
        if (i < locations.Count)
        {
            attack = true;
            target = locations[i];
            movementSpeed = speeds[i];
            yield return new WaitUntil(() => (Vector2)transform.position == locations[i]);
            yield return new WaitForSeconds(0.05f);
            i++;
            StartCoroutine(Attack());
        }
        else
        {
            i = 0;
            attack = false;
            yield return new WaitForSeconds(Random.Range(5, 10));                       
            if (!IsInsideBounds())
            {
                StartCoroutine(Chase(GeneratePointInBounds()));
            }
            else
            {
                StartCoroutine(Wander());
            }
        }
    }

    private IEnumerator Chase(Vector2 newTarget)
    {
        chase = true;
        target = newTarget;
        yield return new WaitUntil(() => IsInsideBounds());
        yield return new WaitForSeconds(0.2f);
        chase = false;
        StartCoroutine(Wander());       
    }

    private IEnumerator Wander()
    {
        wander = true;
        int dir = RandomDir();
        SetDirection(dir);

        target = (Vector2)transform.position + new Vector2(RandomDir() * RandomDistance(5, 10), RandomDir() * RandomDistance(5, 10));
        movementSpeed = RandomSpeed(3, 10);

        yield return new WaitForSeconds(RandomTime(3, 6));  
        if(!IsInsideBounds())
        {
            StartCoroutine(Chase(GeneratePointInBounds()));
        }
        else
        {
            StartCoroutine(Attack());
        }
        wander = false;
    }


    public int RandomDir()
    {
        int num = Random.Range(-1, 2);
        if (num == 0) { return -1; }
        else { return num; }
    }
    public int RandomSpeed(int MIN, int MAX)
    {
        return Random.Range(MIN, MAX);
    }
    public void SetDirection(int dir)
    {      
        gameObject.transform.localScale = new Vector3(dir * Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);    
    }
    //public float GetDistanceFromPlayer()
    //{       
    //    return Vector2.Distance(player.transform.position, gameObject.transform.position);
    //}
    public float RandomTime(float MIN, float MAX)
    {
        return Random.Range(MIN, MAX);
    }
    public float RandomDistance(float MIN, float MAX)
    {
        return Random.Range(MIN, MAX);
    }
    public bool IsInsideBounds()
    {
        float posX = transform.position.x, posY = transform.position.y;       
        if(posX > Bottom_L.x && posX < Bottom_R.x && posY > Bottom_L.y && posY < Top_L.y) {return true;}
        else{return false;}
    }
    public Vector2 GeneratePointInBounds()
    {
        return new Vector2(Random.Range(Bottom_L.x, Bottom_R.x), Random.Range(Bottom_L.y, Top_L.y));
    }


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
