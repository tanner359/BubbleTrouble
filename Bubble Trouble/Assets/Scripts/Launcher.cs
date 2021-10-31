using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;

    [SerializeField] public GameObject gameOverMenu;
    [SerializeField] public GameObject optionsMenu;


    public Animator CrossFade;
    public Animator BubbleFade;
    public bool completeLoad = false;

    private void Awake()
    {       
        instance = this;       
    }
    private void Start()
    {
        BubbleFade = GameObject.Find("BubbleFade").GetComponent<Animator>();        
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        StartCoroutine(Load(level));
    }

    public IEnumerator Load(int level)
    {
        FadeOut();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        asyncLoad.allowSceneActivation = false;
        while (!completeLoad)
        {
            yield return null;
        }             
        FadeIn();
        yield return new WaitForSeconds(1f);
        asyncLoad.allowSceneActivation = true;
        completeLoad = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
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

    public void ToggleOptions()
    {

        Debug.Log("toggle options");
        if (optionsMenu.activeSelf.Equals(true))
        {
            optionsMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            optionsMenu.SetActive(true);
            Time.timeScale = 0;
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
