using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;

    [SerializeField] public GameObject gameOverMenu;
    public Animator CrossFade;
    public Animator BubbleFade;
    public bool isLoading = false;

    private void Awake()
    {       
        instance = this;       
    }
    private void Start()
    {
        BubbleFade = GameObject.Find("BubbleFade").GetComponent<Animator>();
        FadeIn();
    }
    public void LoadLevel(int level)
    {
        StartCoroutine(Load(level));
    }

    public IEnumerator Load(int level)
    {
        FadeOut();
        yield return new WaitUntil(() => isLoading == true);
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void OpenMenu(GameObject menuToOpen)
    {
        menuToOpen.SetActive(true);   
    }
    public void CloseMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
    }
    public void PauseGame(bool state)
    {
        if (state)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void GameOver()
    {
        OpenMenu(gameOverMenu);
        gameOverMenu.GetComponent<Animator>().SetTrigger("GameOver");
    }
    public void FadeOut()
    {
        CrossFade.SetTrigger("FadeOut");
        BubbleFade.SetTrigger("FadeOut");
    }
    public void FadeIn()
    {
        CrossFade.SetTrigger("FadeIn");
        BubbleFade.SetTrigger("FadeIn");
    }
}
