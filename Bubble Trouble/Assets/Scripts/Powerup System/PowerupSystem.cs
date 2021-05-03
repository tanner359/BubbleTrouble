using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PowerupSystem
{
    public static bool spawnPwr = false;
    public static bool toxicPwr = false;
    public static bool piercePwr = false;

    public static Transform cooldownShelf = GameObject.FindGameObjectWithTag("CooldownShelf").transform;
    public static GameObject spawnUI = Resources.Load<GameObject>("Cooldown UI/SpawnCooldownUI");
    public static GameObject toxicUI = Resources.Load<GameObject>("Cooldown UI/ToxicCooldownUI");
    public static GameObject speedUI = Resources.Load<GameObject>("Cooldown UI/SpeedCooldownUI");
    public static GameObject pierceUI = Resources.Load<GameObject>("Cooldown UI/PierceCooldownUI");

    public static IEnumerator StartPowerup(Powerup.Type type)
    {
        switch (type)
        {
            case Powerup.Type.Toxic:
                if (toxicPwr) break;
                GameObject toxicIcon = Object.Instantiate(toxicUI, cooldownShelf);
                toxicIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupIcons/ToxicPwrupIcon");
                toxicPwr = true;
                Debug.Log("Toxic effects started");
                yield return new WaitForSeconds(10f);
                toxicPwr = false;
                Debug.Log("Toxic effects ended");
                break;
            case Powerup.Type.Speed:
                if (PlayerController.hitForce > 51f) break;
                GameObject speedIcon = Object.Instantiate(speedUI, cooldownShelf);
                speedIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupIcons/SpeedPwrupIcon");
                PlayerController.hitForce += (PlayerController.hitForce * 2f);
                Debug.Log("Speed effects started");
                yield return new WaitForSeconds(10f);
                PlayerController.hitForce = 50f;
                Debug.Log("Speed effects ended");
                break;
            case Powerup.Type.Spawn:
                if (spawnPwr) break;
                spawnPwr = true;
                GameObject spawnIcon = Object.Instantiate(spawnUI, cooldownShelf);
                spawnIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupIcons/SpawnPwrupIcon");
                Debug.Log("Spawn effects started");
                yield return new WaitForSeconds(10f);
                spawnPwr = false;
                Debug.Log("Spawn effects 'ended'");
                break;
            case Powerup.Type.Pierce:
                if (piercePwr) break;
                piercePwr = true;
                GameObject pierceIcon = Object.Instantiate(pierceUI, cooldownShelf);
                pierceIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("PowerupIcons/PiercePwrupIcon");
                Debug.Log("Pierce effects started");
                yield return new WaitForSeconds(10f);
                spawnPwr = false;
                Debug.Log("Pierce effects 'ended'");
                break;
        }
    }

/*    if (collision.gameObject.CompareTag("SpeedPwrup"))
    {
        hitForce = 150f;
        timer = 10f;
        Pipe.bubbleSpeedPwr = true;
        //Pipe.bubbleSpawnPwr = false;
        speedCooldown.gameObject.SetActive(true);
    }

    if (collision.gameObject.CompareTag("SpawnPwrup"))
    {
        *//*Pipe.bubbleSpawnPwr = true;
        //Pipe.bubbleSpeedPwr = false;
        timer = 10f;
        spawnCooldown.gameObject.SetActive(true);*//*
        StartCoroutine(SpawnPwrup());
    }

    if (collision.gameObject.CompareTag("ToxicPwrup"))
    {
        Pipe.bubbleToxicPwr = true;
        timer = 10f;
        toxicCooldown.gameObject.SetActive(true);
    }
*/}
