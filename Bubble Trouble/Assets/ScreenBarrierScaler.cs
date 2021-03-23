using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBarrierScaler : MonoBehaviour
{
    public BoxCollider2D TOP, BOTTOM, LEFT, RIGHT;
    public int resolutionX;
    public int resolutionY;


    private void Awake()
    {
        resolutionX = Screen.width;
        resolutionY = Screen.height;

        TOP.size = new Vector2(resolutionX, 1);
        BOTTOM.size = new Vector2(resolutionX, 1);

        LEFT.size = new Vector2(1, resolutionY);
        RIGHT.size = new Vector2(1, resolutionY);
    }
}
