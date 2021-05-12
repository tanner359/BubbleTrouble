using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class PowerupSpawning
{
    public static float spawnChance = 0.075f;
    public static GameObject[] powerups = Resources.LoadAll<GameObject>("Powerups");

    public static void SpawnRandom(Vector2 pos)
    {
        float i = Random.Range(0f, 1f);
        if (i <= spawnChance) {
            Object.Instantiate(powerups[Random.Range(0, powerups.Length)], pos, Quaternion.identity);
            
            spawnChance = 0.075f;
        }
        else { spawnChance += 0.05f; }
    }
}
