using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bubble Modifier")]
public class BubbleModifier : ScriptableObject
{
    public int HitDamage;
    public int DotDamage;

    public Sprite sprite;
}
