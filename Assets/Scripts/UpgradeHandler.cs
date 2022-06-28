using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : Character
{

    //Remove the upgrade from the map by settings it to inactive
    public void removeUpgrade()
    {
        gameObject.SetActive(false);
    }

    //Trigger animation for collecting the upgrade if the player enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
      
        if(other.gameObject.tag == "Player")
        {
 
            Animator.SetTrigger("Collection");
            
        }
    }
}
