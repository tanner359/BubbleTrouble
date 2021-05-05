using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Level_ID;
    public bool levelCleared;

    public Spawn.Wave currentWave;

    public GameObject roundBanner;
    Animator roundAnim;
    Image roundImage;

    //REFERENCE TO THE WORLD CLEAR BANNER
    public GameObject WorldClearBanner;

    private void Awake()
    {
        instance = this;      
    }

    private void Start()
    {
        PowerupSystem.cooldownShelf = GameObject.FindGameObjectWithTag("CooldownShelf").transform;

        roundAnim = roundBanner.GetComponent<Animator>();
        roundImage = roundBanner.transform.Find("WaveText").GetComponent<Image>();

        StartCoroutine(ActivateWaveBanner());
    }
    public void NextWave()
    {
        currentWave++;
        StartCoroutine(ActivateWaveBanner());
    }

    public void WorldCleared()
    {
        levelCleared = true;
        StartCoroutine(ExitScene());
    }
    public IEnumerator ActivateWaveBanner()
    {
        switch (currentWave)
        {
            case Spawn.Wave.Wave_1:
                roundImage.sprite = Resources.LoadAll<Sprite>("Sprites/Wave1Banner")[1];
                break;

            case Spawn.Wave.Wave_2:
                roundImage.sprite = Resources.LoadAll<Sprite>("Sprites/Wave2Banner")[1];
                break;

            case Spawn.Wave.Wave_3:
                roundImage.sprite = Resources.LoadAll<Sprite>("Sprites/Wave3Banner")[1];
                break;

            case Spawn.Wave.Boss:
                roundImage.sprite = Resources.Load<Sprite>("Sprites/BossWaveBanner");
                break;
        }
        roundBanner.SetActive(true);
        roundAnim.SetTrigger("RoundBanner");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => roundAnim.GetCurrentAnimatorStateInfo(0).IsName("RoundBanner") == false);
        roundBanner.SetActive(false);

        Spawn.instance.StartWave(currentWave);
    }

    public IEnumerator ExitScene()
    {
        PlayerData.UnlockWorld(Level_ID++);

        //Activate the World Banner once the Boss is defeated.
        WorldClearBanner.SetActive(true);

        yield return new WaitForSeconds(4f);
        GameProperties.ChangeWorldLightIntesity(0, 0.02f);
        yield return new WaitUntil(() => GamePropertyManager.instance.WorldLight.intensity <= 0.2f);
        Launcher.instance.LoadLevel(1);
    }

}
