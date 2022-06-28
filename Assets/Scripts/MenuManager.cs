using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    //Starts game by swapping to level 1 scene
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    //Quits game
    public void ExitGame()
    {
        Application.Quit();
    }

}
