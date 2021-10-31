using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public static class Combat
{
    public static GameObject combatText_Prefab = Resources.Load<GameObject>(Path.Combine("Prefabs", "CombatText"));
    public static Transform worldCanvas = GameObject.Find("World_Canvas").transform;

    public static void DamageTarget(Enemy target, int damage)
    {
        target.Health -= damage;
        SpawnCombatText(Color.red, damage, 1.5f, target.transform.position + new Vector3(0, 3, 0));    
    }

    public static void DamageOverTime(Enemy target, int damage, float time, float tickRate)
    {
        target.GetComponent<DamageHandler>().ApplyDamageOverTime(target, damage, time, tickRate);
    }
    public static void SpawnCombatText(Color _color, int _damage, float _duration, Vector3 _location)
    {
        CombatText.CombatTextInfo(_color, _damage, _duration);
        Object.Instantiate(combatText_Prefab, _location, Quaternion.identity, worldCanvas);
    }
}

public class Effects : MonoBehaviour
{
    public void DamageOverTime()
    {
        
    }
}


