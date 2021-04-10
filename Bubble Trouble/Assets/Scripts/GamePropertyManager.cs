using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GamePropertyManager : MonoBehaviour
{
    public Light2D WorldLight;

    private void Update()
    {
        WorldLight.intensity = Mathf.Lerp(WorldLight.intensity, GameProperties.WorldLightIntesity, 2f);
    }

}
public static class GameProperties
{
    public static float WorldLightIntesity { get; private set; } = 10f;

    public static void ChangeWorldLightIntesity(float value)
    {
        WorldLightIntesity = value;     
    }     
}

