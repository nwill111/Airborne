using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    //Set game over screen to active
    public void GameOverScreen()
    {
        gameObject.SetActive(true);
    }

    //Set game over screen to inactive
    public void HideGameOver()
    {
        gameObject.SetActive(false);
    }

}
