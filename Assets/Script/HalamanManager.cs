using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HalamanManager : MonoBehaviour
{
    public bool isEscapeToExit;

    
    void Start()
    {

    }

   
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscapeToExit)
            {
                Application.Quit();
            }
            else
            {
                KembaliKeMenu();
            }
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void KembaliKeMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ExitMenu()
    {
        SceneManager.LoadScene("ExitGame");
    }
    public void CreditMenu()
    {
        SceneManager.LoadScene("Credit");
    }
    public void ExitGamePlay()
    {
        Application.Quit();
    }

}
