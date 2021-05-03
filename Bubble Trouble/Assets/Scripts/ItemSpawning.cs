using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ItemSpawning
{
    public static float spawnChance = 0.05f;
    public static GameObject[] powerups = Resources.LoadAll<GameObject>("Powerups");

    public static void SpawnRandom(Vector2 pos)
    {
        float i = Random.Range(0f, 1f);
        if (i <= spawnChance) {
            Object.Instantiate(powerups[Random.Range(0, powerups.Length - 1)], pos, Quaternion.identity);
            
            spawnChance = 0.05f;
        }
        else { spawnChance += 0.025f; }
    }
}
