using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectAnchor : MonoBehaviour
{
    public Vector2 anchor;

    public Vector2 offset;
   
    void Update()
    {
        anchor = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        transform.position = anchor + offset;
        
    }
}
