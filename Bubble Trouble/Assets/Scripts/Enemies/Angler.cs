using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angler : Enemy
{
    private void Awake()
    {
        GameProperties.ChangeWorldLightIntesity(2, 0.025f);
    }

    private void OnDestroy()
    {
        GameProperties.ChangeWorldLightIntesity(10, 0.025f);
    }
}
