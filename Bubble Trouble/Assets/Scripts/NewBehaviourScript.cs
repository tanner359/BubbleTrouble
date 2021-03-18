using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public Sprite[] tiles;
    public SpriteRenderer sr;
    float distance = 50; // temp number
    public ContactFilter2D rayMask;
    public RaycastHit2D[] northResults, southResults, eastResults, westResults;
    public int tileValue = 0;


    public void ReduceTileValue(int reductionAmount)
    {
        tileValue = tileValue - reductionAmount;
    }

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnPress()
    {
        Debug.Log("you clicked the button");

        //North
        if(Physics2D.Raycast(transform.position, Vector2.up * distance, rayMask, northResults) > 0)
        {
            for(int i = 0; i < northResults.Length; i++)
            {
                northResults[i].collider.gameObject.GetComponent<SpriteRenderer>().sprite = tiles[tileValue];
                northResults[i].collider.gameObject.GetComponent<TestButton>().ReduceTileValue(1);
            }
        }
        //South
        if (Physics2D.Raycast(transform.position, Vector2.down * distance, rayMask, southResults) > 0)
        {
            for (int i = 0; i < southResults.Length; i++)
            {
                southResults[i].collider.gameObject.GetComponent<SpriteRenderer>().sprite = tiles[tileValue];
                southResults[i].collider.gameObject.GetComponent<TestButton>().ReduceTileValue(1);
            }
        }       
        //East
        if (Physics2D.Raycast(transform.position, Vector2.right * distance, rayMask, eastResults) > 0)
        {
            for (int i = 0; i < eastResults.Length; i++)
            {
                eastResults[i].collider.gameObject.GetComponent<SpriteRenderer>().sprite = tiles[tileValue];
                eastResults[i].collider.gameObject.GetComponent<TestButton>().ReduceTileValue(1);
            }
        }     
        //West
        if (Physics2D.Raycast(transform.position, Vector2.left * distance, rayMask, westResults) > 0)
        {
            for (int i = 0; i < westResults.Length; i++)
            {
                westResults[i].collider.gameObject.GetComponent<SpriteRenderer>().sprite = tiles[tileValue];
                westResults[i].collider.gameObject.GetComponent<TestButton>().ReduceTileValue(1);
            }
        }     
    }
}