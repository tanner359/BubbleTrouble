using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public Enemy enemy;

    public void ApplyDamageOverTime(Enemy target, int damage, float time, float tickRate)
    {
        if(t <= 0)
        {
            StartCoroutine(DamageOverTime(target, damage, time, tickRate));
            return;
        }
        else if(t > 0)
        {
            if(t + time > 10)
            {
                t = 10;
                return;
            }
            t += time;
        }
    }

    public float t = 0;
    public IEnumerator DamageOverTime(Enemy target, int damage, float time, float tickRate)
    {
        t = time;

        while (t > 0)
        {
            yield return new WaitForSeconds(tickRate);
            target.Health -= damage;
            Combat.SpawnCombatText(Color.red, damage, 1.5f, target.transform.position + new Vector3(0, 3, 0));
            t -= tickRate;
        }
    }
}
