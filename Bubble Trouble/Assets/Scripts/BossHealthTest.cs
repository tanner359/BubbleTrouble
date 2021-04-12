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
        transform.SetParent(GameObject.FindWithTag("Canvas").transform);   
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

    }


    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    //If the bubble touches the Boss,
    //    //Call the DecreaseBossHealth script to decrease the health of the boss,
    //    //And delete the Bubble.
    //    if(other.tag == "Bubble")
    //    {
    //        //Can change the value of this later, 0.20 (20%) is set for debug/testing purposes 
    //        DecreaseBossHealth(0.20f);
    //        Destroy(other.gameObject);

    //        if (currentBossHealthValue <= 0f)
    //        {
    //            Debug.Log("test222");
    //            bossHealthBarFill.fillAmount = 0f;
    //            bossHealthBar.gameObject.SetActive(false);
    //            bossHealthBarFill.gameObject.SetActive(false);
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
    //public void DecreaseBossHealth(float percentage)
    //{
    //    Debug.Log("Bubble Hit the Boss");

    //    /*If the BossHealth Bar is not = 0,
    //     *Set the fill amount of the Boss's Health Bar by subtracting the current Boss Health value from the determined percentage 
    //     *Set the current Boss Health value to be its current value subtracted by the percentage 
    //     *If the currentBossHealthValue is less than or equal to 0,
    //     *Set the currentBossHealthValue AND the bossHealthBarrFill fill amount to be 0 
    //     * 
    //     *Get the percentage of the maximum filled amount (what % out of 100, represented as a decimal #)
    //     *Lower the Boss's Health Bar by the percentage of damage the bubble does to the Boss (set to 3 for now)
    //     * 
    //     */

    //    bossHealthBarFill.fillAmount = currentBossHealthValue / maximumHealthValue;
    //    currentBossHealthValue = currentBossHealthValue - percentage;

    //    if(bossHealthBarFill.fillAmount <= 0f)
    //    {
    //        Debug.Log("Johnny Test");
    //        bossHealthBarFill.fillAmount = 0f;
    //        currentBossHealthValue = 0f;
    //    }
    //}
}
