using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxHandler : MonoBehaviour
{
    public Player player;
    
    //Handles DeathBoxes
    private void OnTriggerEnter2D(Collider2D other)
    {
      
        //If entered and is the player
        if(other.gameObject.tag == "Player")
        {
            //Deals 1000 damage to the player
            player.TakeDamage(1000);
        }
    }

}
