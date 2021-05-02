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

    [Space(10)]
    [Header("AI Movement")]
    public Enemy_Type enemyType;
    [Space(50)]
    [SerializeField] public List<Vector2> locations;
    public List<float> speeds;

    public bool rotate;

    [Space(10)]
    [Header("Current Movement Phase")]
    public bool attack = false;
    public bool wander = false;
    public bool move = false;
    public bool follow = false;

    [Header("Attack Properties")]
    public int triggerPosition = 1;

    public GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        renderers = body.GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (IsInsideBounds())
        {
            StartCoroutine(Wander());
        }
        else
        {
            StartCoroutine(MoveToBounds());
        }          
    }

    public void UpdateValues()
    {
        //updates the values in the editor DO NOT REMOVE THIS!!
    }

    public void Update()
    {
        #region BOUNDRY POSITION CALCULATIONS
        Bottom_L = Camera.main.ScreenToWorldPoint(new Vector3(0 + MIN_X, 0 + MIN_Y, Camera.main.transform.position.z));
        Top_L = Camera.main.ScreenToWorldPoint(new Vector3(0 + MIN_X, Screen.height + MAX_Y, Camera.main.transform.position.z));
        Bottom_R = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + MAX_X, 0 + MIN_Y, Camera.main.transform.position.z));
        Top_R = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + MAX_X, Screen.height + MAX_Y, Camera.main.transform.position.z));

        Debug.DrawLine(Bottom_L, Bottom_R, Color.red);
        Debug.DrawLine(Bottom_R, Top_R, Color.red);
        Debug.DrawLine(Top_R, Top_L, Color.red);
        Debug.DrawLine(Bottom_L, Top_L, Color.red);
        #endregion

        #region UPDATE LOCATIONS RELATIVE POSITION
        if (locations[0] != (Vector2)transform.position && !attack)
        {
            Vector2 diff = (Vector2)transform.position - locations[0];
            locations[0] = transform.position;

            for (int i = 1; i < locations.Count; i++)
            {
                locations[i] += diff;
            }
        }
        #endregion

        if (rotate)
        {
            SetDirection();
        }     
    }

    private void FixedUpdate()
    {       
        if (attack || wander || move)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
            if (!IsInsideBounds() && !attack && !move)
            {
                StopAllCoroutines();
                move = true;
                StartCoroutine(MoveToBounds());             
            }
        }
        else if (follow)
        {
            target = new Vector2(player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
        }        

    }
    #region STATE MACHINE
    int i = 0;
    private IEnumerator Attack()
    {
        if(i == 0 && enemyType == Enemy_Type.Melee)
        {
            StartCoroutine(FollowPlayer());
            yield return new WaitUntil(() => follow == false);
        }
        
        #region Animations
        if (i == triggerPosition) { GetComponent<Animator>().SetTrigger("Attack_01"); }
        #endregion
        if (i < locations.Count)
        {
            attack = true;
            target = locations[i];
            movementSpeed = speeds[i];
            yield return new WaitUntil(() => (Vector2)transform.position == locations[i]);
            i++;           
            StartCoroutine(Attack());
        }
        else if(i == locations.Count)
        {
            i = 0;
            attack = false;
            yield return new WaitForSeconds(RandomTime(2, 4));
            StartCoroutine(Wander());
        }
    }

    private IEnumerator FollowPlayer()
    {
        follow = true;
        movementSpeed = 16f;
        yield return new WaitForSeconds(2f);
        follow = false;
    }

    private IEnumerator MoveToBounds()
    {
        movementSpeed = 10f;
        target = GeneratePointInBounds();
        move = true;     
        yield return new WaitUntil(() => IsInsideBounds());
        yield return new WaitForSeconds(0.2f);
        move = false;
        StartCoroutine(Wander());
    }
  
    private IEnumerator Wander()
    {
        wander = true;
        target = GeneratePointInBounds();
        movementSpeed = RandomSpeed(3, 8);      
        yield return new WaitUntil(() => (Vector2)transform.position == target);
        wander = false;
        StartCoroutine(Attack());
    }
    #endregion

    #region AI FUNCTIONS
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
    public void SetDirection()
    {
        Vector3 dir = target - (Vector2)transform.localPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle + 180, Vector3.forward), 0.2f);

        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

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
        if(posX >= Bottom_L.x && posX <= Bottom_R.x && posY >= Bottom_L.y && posY <= Top_L.y) {return true;}
        else{return false;}
    }
    public Vector2 GeneratePointInBounds()
    {
        return new Vector2(Random.Range(Bottom_L.x, Bottom_R.x), Random.Range(Bottom_L.y, Top_L.y));
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble") || collision.CompareTag("Explosion"))
        {
            if (collision.CompareTag("Bubble")) Destroy(collision.gameObject);
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
                ItemSpawning.SpawnRandom(transform.position);
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
