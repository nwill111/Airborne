using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles code for the checkpoints in the level
public class CheckpointHandler : MonoBehaviour
{

    public Player player;
    public SpriteRenderer SpriteRenderer;
    public Sprite checkpointActive;
    private bool inactive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
      
        //If the player enters the hitbox for the checkpoint...
        if(other.gameObject.tag == "Player")
        {

            if (inactive)
            {

                //Set the checkpoint's sprite to active
                SpriteRenderer.sprite = checkpointActive;

                //Heal player
                player.Heal();

                //Set checkpoint to active
                inactive = false;
            }

         
        }
    }
}
