using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    [SerializeField] public static int volume = 50;

    [SerializeField] public static int sensitivity = 50;


    public static void ChangeVolume(int value)
    {
        volume = value;
    }

    public static void ChangeSensitivity(int value)
    {
        sensitivity = value;
    }
}
