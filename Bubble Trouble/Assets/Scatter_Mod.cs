using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatter_Mod : MonoBehaviour
{
    public int HitDamage = 5;
    public int numOfChildren = 10;
    public int ChildDamage = 1;

    public int divisions = 0;

    Stats stats;

    bool isChild = false;


    private void OnEnable()
    {
        if (!isChild)
        {
            stats = gameObject.GetComponent<Stats>();
            stats.AddStats(HitDamage, 0);
            return;
        }
    }

    private void Start()
    {
        if(TryGetComponent(out Toxic_Mod toxicMod))
        {
            toxicMod.radius = transform.localScale.x * 25f;
        }
        StartCoroutine(ActiveDelay(0.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (!enabled) { return; }

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("test2");
            if (divisions < 1)
            {
                divisions++;
                for (int i = 0; i < numOfChildren; i++)
                {
                    Debug.Log("Spawn Child");
                    GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity);
                    child.GetComponent<Scatter_Mod>().isChild = true;
                    child.transform.localScale *= Random.Range(0.15f, 0.25f);
                    child.GetComponent<Stats>().HitDamage = ChildDamage;
                    child.GetComponent<CircleCollider2D>().enabled = false;
                    child.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000)));
                }
            }
            Destroy(gameObject, 0.01f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) { return; }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject, 0.01f);
        }
    }

    public IEnumerator ActiveDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnDisable()
    {
        stats.RemoveStats(HitDamage, 0);
    }
}
