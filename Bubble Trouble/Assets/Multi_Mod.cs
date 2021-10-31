using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multi_Mod : MonoBehaviour
{
    public int HitDamage = 5;

    public int numOfChildren = 1;
    public int numOfDivisions = 1;

    public int currentDivision = 0;

    Stats stats;

    private void OnEnable()
    {
        stats = gameObject.GetComponent<Stats>();
        stats.AddStats(HitDamage, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) { return; }

        if (PlayerController.instance.IsAttacking())
        {
            if (currentDivision < numOfDivisions)
            {
                if (collision.CompareTag("Claw"))
                {
                    currentDivision++;
                    for (int i = 0; i < numOfChildren; i++)
                    {
                        Instantiate(gameObject, transform.position, Quaternion.identity);
                    }
                }
            }
        }
        
        if(collision.CompareTag("Enemy"))
        {
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

    private void OnDisable()
    {
        stats.RemoveStats(HitDamage, 0);
    }
}
