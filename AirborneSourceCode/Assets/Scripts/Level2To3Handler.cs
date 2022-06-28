using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2To3Handler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        //Trigger level change from 2 to 3
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("End");
        }
    }
}
