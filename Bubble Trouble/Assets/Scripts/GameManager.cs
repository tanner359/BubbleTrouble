using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Spawn.Wave currentWave;

    public GameObject roundBanner;
    Animator roundAnim;
    Image roundImage;

    private void Awake()
    {
        instance = this;      
    }

    private void Start()
    {
        roundAnim = roundBanner.GetComponent<Animator>();
        roundImage = roundBanner.transform.Find("WaveText").GetComponent<Image>();

        StartCoroutine(ActivateWaveBanner());
    }

    public void NextWave()
    {
        currentWave++;
        StartCoroutine(ActivateWaveBanner());
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
}
