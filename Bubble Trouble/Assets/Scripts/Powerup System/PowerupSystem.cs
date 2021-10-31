using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PowerupSystem
{
    public static float spawnChance = 0.05f; // base: 5% chance (+2%)
    public static GameObject[] powerups = Resources.LoadAll<GameObject>("Powerups");

    public static void SpawnRandom(Vector2 pos)
    {
        float i = Random.Range(0f, 1f);
        if (i <= spawnChance)
        {
            Object.Instantiate(powerups[Random.Range(0, powerups.Length)], pos, Quaternion.identity);

            spawnChance = 0.05f;
        }
        else { spawnChance += 0.02f; }
    }

    public static IEnumerator ActivatePowerup(Powerup.Type type)
    {
        switch (type)
        {
            case Powerup.Type.Toxic:
                
                break;
            case Powerup.Type.Speed:
                
                break;
            case Powerup.Type.Spawn:
                
                break;
            case Powerup.Type.Pierce:
                
                break;
        }
        return null;
    }

    public static void ClearEffects()
    {
        // clear current power up effects
    }
}
