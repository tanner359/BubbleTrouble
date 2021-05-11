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

    //public IEnumerator LoadAsyncScene(int level)
    //{
    //    // The Application loads the Scene in the background as the current Scene runs.
    //    // This is particularly good for creating loading screens.
    //    // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
    //    // a sceneBuildIndex of 1 as shown in Build Settings.

    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
    //    // Wait until the asynchronous scene fully loads
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }
    //}

    public void RestartLevel()
    {
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
