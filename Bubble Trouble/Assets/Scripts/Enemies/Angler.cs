using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angler : Enemy
{
    private void Awake()
    {
        GameProperties.ChangeWorldLightIntesity(2);
    }

    private void OnDestroy()
    {
        GameProperties.ChangeWorldLightIntesity(10);
    }
}
