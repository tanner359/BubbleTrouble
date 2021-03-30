using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameObjectAnchor : MonoBehaviour
{
    public enum Alignment {Top, Bottom, Center, Left, Right}

    public Alignment anchorAlignment;
    public Vector2 anchor;
    public Vector2 offset;
   
    void Update()
    {
        switch (anchorAlignment)
        {
            case Alignment.Top:
                anchor = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, Camera.main.transform.position.z));
                break;

            case Alignment.Bottom:
                anchor = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, Camera.main.transform.position.z));
                break;

            case Alignment.Left:
                anchor = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, Camera.main.transform.position.z));
                break;

            case Alignment.Right:
                anchor = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, Camera.main.transform.position.z));
                break;

            case Alignment.Center:
                anchor = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.transform.position.z));
                break;
        }     
        transform.position = anchor + offset;       
    }
}
