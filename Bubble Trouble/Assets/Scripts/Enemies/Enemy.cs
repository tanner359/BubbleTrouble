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
    public Vector2 Bottom_L;
    Vector2 BL_Offset;
    public Vector2 Top_L;
    Vector2 TL_Offset;
    public Vector2 Bottom_R;
    Vector2 BR_Offset;
    public Vector2 Top_R;
    Vector2 TR_Offset;

    Vector2 leftRefAnchor;
    Vector2 rightRefAnchor;

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
        BL_Offset = Bottom_L;
        TL_Offset = Top_L;
        BR_Offset = Bottom_R;
        TR_Offset = Top_R;

        leftRefAnchor = Camera.main.ScreenToWorldPoint(new Vector3(0, 2400f / 2, Camera.main.transform.position.z));
        rightRefAnchor = Camera.main.ScreenToWorldPoint(new Vector3(1080f, 2400f / 2, Camera.main.transform.position.z));

        player = GameObject.FindGameObjectWithTag("Player");
        renderers = body.GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (IsInsideBounds(transform.position))
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
        #region BOUNDRY SCREEN SCALE
        Vector2 leftAnchor = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, Camera.main.transform.position.z));
        Vector2 rightAnchor = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, Camera.main.transform.position.z));

        Bottom_L =  new Vector2(leftAnchor.x + (BL_Offset.x - leftRefAnchor.x), BL_Offset.y);

        Bottom_R = new Vector2(rightAnchor.x + (BR_Offset.x - rightRefAnchor.x), BR_Offset.y);

        Top_L = new Vector2(leftAnchor.x + (TL_Offset.x - leftRefAnchor.x), TL_Offset.y);

        Top_R = new Vector2(rightAnchor.x + (TR_Offset.x - rightRefAnchor.x), TR_Offset.y);

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
            if (!IsInsideBounds(transform.position) && !attack && !move)
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
        yield return new WaitUntil(() => IsInsideBounds(transform.position));
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
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), -Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
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
    public bool IsInsideBounds(Vector2 pos)
    {
        float posX = transform.position.x, posY = transform.position.y;       
        if(posX >= Bottom_L.x && posX <= Bottom_R.x && posX <= Top_R.x && posX >= Top_L.x 
        && posY >= Bottom_L.y && posY >= Bottom_R.y && posY <= Top_L.y && posY <= Top_R.y) {return true;}
        else{return false;}
    }
    public Vector2 GeneratePointInBounds()
    {
        float x1 = Random.Range(Bottom_L.x, Top_L.x);
        float m1 = (Top_L.y - Bottom_L.y) / (Top_L.x - Bottom_L.x);
        float b1 = -1*(m1 * Bottom_L.x) + Bottom_L.y;
        Vector2 Left = new Vector2(x1, m1 * x1 + b1);

        float x2 = Random.Range(Top_L.x, Top_R.x);
        float m2 = (Top_R.y - Top_L.y) / (Top_R.x - Top_L.x);
        float b2 = -1 * (m2 * Top_L.x) + Top_L.y;
        Vector2 Top = new Vector2(x2, m2 * x2 + b2);

        float x3 = Random.Range(Bottom_R.x, Top_R.x);
        float m3 = (Bottom_R.y - Top_R.y) / (Bottom_R.x - Top_R.x);
        float b3 = -1 * (m3 * Bottom_R.x) + Bottom_R.y;
        Vector2 Right = new Vector2(x3, m3 * x3 + b3);

        float x4 = Random.Range(Bottom_L.x, Bottom_R.x);
        float m4 = (Bottom_R.y - Bottom_L.y) / (Bottom_R.x - Bottom_L.x);
        float b4 = -1 * (m4 * Bottom_L.x) + Bottom_L.y;
        Vector2 Bottom = new Vector2(x4, m4 * x4 + b4);

        float x5 = 0;
        float m5 = (Right.y - Left.y) / (Right.x - Left.x);
        float b5 = -1 * (m5 * Left.x) + Left.y;
        float Line1 = m5 * x5 + b5;

        float x6 = 0;
        float m6 = (Top.y - Bottom.y) / (Top.x - Bottom.x);
        float b6 = -1 * (m6 * Bottom.x) + Bottom.y;
        float Line2 = m6 * x6 + b6;

        float final_X = (b6 - b5) / (m5 - m6);
        float final_Y = m6 * final_X + b6;

        return new Vector2(final_X, final_Y);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble") || collision.CompareTag("Explosion"))
        {
            if (!collision.gameObject.GetComponent<Bubble>().wasHit) { return; }
            if (collision.CompareTag("Bubble") && !PowerupSystem.piercePwr) Destroy(collision.gameObject);
            Health--;
            if(Health != 0)
            {
                if (healthFlash == false)
                {
                    StartCoroutine(DamageFlash(renderers, renderers[0].color, Color.red));
                }
            }
            else
            {
                PowerupSpawning.SpawnRandom(transform.position);
                Spawn.instance.numEnemiesSpawned--;
                Destroy(gameObject);
            }            
        }
    }

    public bool healthFlash;
    public IEnumerator DamageFlash(SpriteRenderer[] renderers, Color origin, Color change)
    {
        healthFlash = true;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = change;  
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = origin;
        }
        healthFlash = false;
    }   
}
