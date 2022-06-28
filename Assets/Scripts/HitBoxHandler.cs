using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxHandler : MonoBehaviour
{

    public Player player;
    public int damage;

    public void disableHitbox()
    {
        gameObject.SetActive(false);
    }

    public void enableHitbox()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Damage player when they enter the hitbox
        if(other.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);

        }
    }



}
