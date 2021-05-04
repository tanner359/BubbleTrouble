using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds all of the current worlds that are locked/unlocked
public class WorldManager : MonoBehaviour
{
    //Lists of the Worlds that we have 
    public List<GameObject> locks = new List<GameObject>();
    //List of the Locks on the Worlds that are 
    //Disable them based on the PlayerData's SaveData to see if they're locked or not 

    //Every time we load a level, 
    //Make sure that the levels displayed are unlocked/up-to-date
    //Put the locks in the same order as the worlds 

    void Awake()
    {
        //Go through each world
        //If the world was beaten, disable that lock
        for(int i = 0; i < PlayerData.worlds.Count; i++)
        {
            if(PlayerData.worlds[i] == true)
            {
                locks[i].SetActive(false);
            }
        }
    }

}
