using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting_Line : MonoBehaviour
{
    Vector2 lineStart;
    Vector2 lineEnd;
    public float lineDistance = 1;
    LineRenderer lineRend;

    private void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {            
        if (collision.CompareTag("Bubble") && !collision.GetComponent<Bubble>().WasBubbleHit())
        {
            Vector2 direction = -(transform.position - collision.transform.position).normalized;
            lineStart = collision.transform.position;
            lineEnd = (Vector2)collision.transform.position + (direction * lineDistance);
            lineRend.SetPosition(0, lineStart);
            lineRend.SetPosition(1, lineEnd);
        }
    }

   
}
