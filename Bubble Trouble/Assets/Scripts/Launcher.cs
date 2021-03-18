using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{

    public void LoadLevel(int level)
    {
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
}
