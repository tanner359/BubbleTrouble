using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ZACH: COPY AND PASTE THIS INTO THE BOSS SCRIPT LATER!
public class BossHealthTest : MonoBehaviour
{
    //Reference to the Boss Health UI on top of the screen///

    //The maximum health for the boss 
    private float maximumHealthValue; 

    //% of the BossHealth which is being decreased 
    private float currentBossHealthValue;

    private float newBossHealthValue; 

    //Outline of the Boss Health UI Bar 
    public Image bossHealthBar;

    //Inside of the Boss Health UI
    public Image bossHealthBarFill;

    //Reference to the Enemy Script
    public Enemy bossEnemy;

    //As soon as this GameObject gets enabled, assign its transform to the canvas 
    private void OnEnable()
    {
        transform.SetParent(GameObject.Find("HealthBarShelf").transform);   
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set the currentBossHealthValue to be equal to the health value of the boss
        maximumHealthValue = bossEnemy.Health;
        currentBossHealthValue = bossEnemy.Health;

        //Find the BossHealthBar and BossHealthBarFill Image GameObjects in the Canvas 
        //bossHealthBar = GameObject.Find("BossHealthBar").GetComponent<Image>();
        //bossHealthBarFill = GameObject.Find("BossHealthBarFill").GetComponent<Image>();

        //Initialize the fill amount for the bossHealthBarFill to be 1 (100%)
        bossHealthBarFill.fillAmount = 1f;
    }

    private void Update()
    {
        //If the boss's health goes down to 0, 
        //Set the BossHealthUI to be inactive
        //And Destroy the Boss's GameObject. 
        newBossHealthValue = bossEnemy.Health; //constantly gets a new health value of the boss's health and updates it 
        if(newBossHealthValue != currentBossHealthValue)
        {
            bossHealthBarFill.fillAmount = currentBossHealthValue / maximumHealthValue;
            currentBossHealthValue = newBossHealthValue;
        }
        if(bossEnemy == null)
        {
            Destroy(gameObject);
        }

    }
}
