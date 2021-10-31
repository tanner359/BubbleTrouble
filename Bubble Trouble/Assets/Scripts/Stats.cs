using UnityEngine;

public class Stats : MonoBehaviour
{
    public int HitDamage = 1;
    public int DotDamage = 0;

    public int totalHitDamage = 1;
    public int totalDotDamage = 0;
    public int numOfMods = 0;

    public void AddStats(int HitDamage, int DotDamage)
    {
        totalHitDamage += HitDamage;
        totalDotDamage += DotDamage;
        numOfMods++;
        CalculateStats();
    }

    public void RemoveStats(int HitDamage, int DotDamage)
    {
        totalHitDamage -= HitDamage;
        totalDotDamage -= DotDamage;
        numOfMods--;
        CalculateStats();
    }

    public void CalculateStats()
    {
        if (numOfMods > 0)
        {
            HitDamage = (int)Mathf.Ceil(totalHitDamage / (float)numOfMods);
            DotDamage = (int)Mathf.Ceil(totalDotDamage / (float)numOfMods);
        }
        else
        {
            HitDamage = totalHitDamage;
            DotDamage = totalDotDamage;
        }
    }
}

