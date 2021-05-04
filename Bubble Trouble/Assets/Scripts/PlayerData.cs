using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THE SAVE DATA FOR THE PLAYER'S ACCOUNT
//Static = never going to change throughout the history of the game being open
public static class PlayerData
{
    //List of the worlds in the game, checks to see if the player has beaten the world or not 
    [SerializeField] public static List<bool> worlds = new List<bool>();

        //When you complete a world, go through the list, set the next world's lock GameObject to be false
    
    //Unlock a World, given the World number worldNumber
    public static void UnlockWorld(int worldNumber)
    {
        //Set index 0 (world 1) to true regardless
        //Then retrieve the data from the list to see which is true and false 
        worlds[worldNumber - 1] = true;


    }

}
