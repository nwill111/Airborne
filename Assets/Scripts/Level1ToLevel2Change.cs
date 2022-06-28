using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1ToLevel2Change : MonoBehaviour
{
    //If the player collides with the change trigger, change to new scene.
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level2");

        }
    }
}
