using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GamePropertyManager : MonoBehaviour
{
    public static GamePropertyManager instance;

    private void Awake()
    {
        instance = this;
    }
    public Light2D WorldLight;

    private void Update()
    {
        WorldLight.intensity = Mathf.Lerp(WorldLight.intensity, GameProperties.WorldLightIntesity, GameProperties.lightSmoothing);
    }

}
public static class GameProperties
{
    public static float WorldLightIntesity { get; private set; } = 10f;
    public static float lightSmoothing { get; private set; } = 0.025f;
    public static void ChangeWorldLightIntesity(float value, float t)
    {
        WorldLightIntesity = value;
        lightSmoothing = t;
    }     
}

