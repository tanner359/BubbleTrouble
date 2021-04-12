using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ZACH: COPY AND PASTE THIS INTO THE BOSS SCRIPT LATER!
public class BossHealthTest : MonoBehaviour
{
    //Reference to the Boss Health UI on top of the screen 

    //% of the BossHealth which is being decreased 
    public float currentBossHealthValue;

    //Outline of the Boss Health UI Bar 
    private Image bossHealthBar;

    //Inside of the Boss Health UI
    private Image bossHealthBarFill;

    // Start is called before the first frame update
    void Start()
    {
        //Set the currentBossHealthValue to be 1 (100%) 
        currentBossHealthValue = 1;

        //Find the BossHealthBar and BossHealthBarFill Image GameObjects in the Canvas 
        bossHealthBar = GameObject.Find("BossHealthBar").GetComponent<Image>();
        bossHealthBarFill = GameObject.Find("BossHealthBarFill").GetComponent<Image>();

        //Initialize the fill amount for the bossHealthBarFill to be 1 (100%)
        bossHealthBarFill.fillAmount = 1;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the bubble touches the Boss,
        //Call the DecreaseBossHealth script to decrease the health of the boss
        if(other.tag == "Bubble")
        {
            //Can change the value of this later, 0.20 (20%) is set for debug/testing purposes 
            DecreaseBossHealth((float)0.20);
        }
    }
    public void DecreaseBossHealth(float percentage)
    {
        Debug.Log("Bubble Hit the Boss");

        //This makes sure the health bar doesn't go negative. 
        if(currentBossHealthValue <= 0)
        {
            currentBossHealthValue = 0;
            bossHealthBarFill.fillAmount = 0;
        }

        /*If the BossHealth Bar is not = 0,
         *Set the fill amount of the Boss's Health Bar by subtracting the current Boss Health value from the determined percentage 
         *Set the current Boss Health value to be its current value subtracted by the percentage 
         *If the currentBossHealthValue is less than or equal to 0,
         *Set the currentBossHealthValue AND the bossHealthBarrFill fill amount to be 0 
         * 
         *Get the percentage of the maximum filled amount (what % out of 100, represented as a decimal #)
         *Lower the Boss's Health Bar by the percentage of damage the bubble does to the Boss (set to 3 for now)
         * 
         */
        
         else if (currentBossHealthValue > 0)
        {
            bossHealthBarFill.fillAmount = currentBossHealthValue - percentage;
            currentBossHealthValue = currentBossHealthValue - percentage;

            if (currentBossHealthValue <= 0)
            {
                currentBossHealthValue = 0;
                bossHealthBarFill.fillAmount = 0;

                //If the boss's health goes down to 0, 
                //Set the BossHealthUI to be inactive
                //And Destroy the Boss's GameObject. 
                bossHealthBar.gameObject.SetActive(false);
                bossHealthBarFill.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }

        }
    }
}
