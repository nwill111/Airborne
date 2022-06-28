using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESCMenuHandler : MonoBehaviour
{

    public GameObject escMenu;
    private bool menuOpened = false;
    

    void Update()
    {
        
        //When ESC is pressed...
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If not already opened...
            if (menuOpened == false)
            {
                //Show ESC menu and pause the game
                escMenu.SetActive(true);
                menuOpened = true;
                Time.timeScale = 0f;
            }
            //IF opened...
            else
            {
                //Hide ESC menu and resume game
                escMenu.SetActive(false);
                menuOpened = false;
                Time.timeScale = 1f;
            }
        }

        //If The menu is opened and enter is pressed, return to main menu
        if (Input.GetKeyDown(KeyCode.Return) && menuOpened)
        {
            SceneManager.LoadScene("MainMenu");
            escMenu.SetActive(false);
            menuOpened = false;
            Time.timeScale = 1f;
        }
        

    }

    //Code for closing game. Im not even sure if I ever use this but Im afraid removing it will break something somewhere else so Im just gonna keep it here
    public void QuitGame()
    {
        Application.Quit();
    }

    //Code for resuming game
    public void Resume()
    {
        escMenu.SetActive(false);
        menuOpened = false;
        Time.timeScale = 1f;
    }
}
