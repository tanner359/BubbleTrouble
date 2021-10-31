using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic_Mod : MonoBehaviour
{
    public int HitDamage = 10;
    public int DotDamage;
    public float tickRate = 1f;
    public int duration = 5;
    public float radius = 10f;

    public GameObject particle;   
    public ContactFilter2D damageFilter;

    public List<Collider2D> targets = new List<Collider2D>();
    Stats stats;
    private void OnEnable()
    {
        stats = gameObject.GetComponent<Stats>();
        stats.AddStats(HitDamage, DotDamage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) { return; }

        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bubble")
        {
            Instantiate(particle, gameObject.transform.position, Quaternion.identity);

            Physics2D.OverlapCircle(gameObject.transform.position, radius, damageFilter, targets);

            for (int i = 0; i < targets.Count; i++)
            {               
                Combat.DamageOverTime(targets[i].GetComponent<Enemy>(), stats.DotDamage, duration, tickRate);             
            }     
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) { return; }

        if (collision.CompareTag("Enemy"))
        {
            Instantiate(particle, gameObject.transform.position, Quaternion.identity);

            Physics2D.OverlapCircle(gameObject.transform.position, radius, damageFilter, targets);

            for (int i = 0; i < targets.Count; i++)
            {
                Combat.DamageTarget(targets[i].GetComponent<Enemy>(), stats.HitDamage);
                Combat.DamageOverTime(targets[i].GetComponent<Enemy>(), stats.DotDamage, duration, tickRate);
            }

            Destroy(gameObject, 0.01f);
        }
    }

    private void OnDisable()
    {
        stats.RemoveStats(HitDamage, DotDamage);
    }
}
