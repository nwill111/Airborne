using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for any general character (enemy, player)
public class Character : MonoBehaviour
{

    public float healthPool = 10f;

    public float speed = 5f;
    public float jumpForce = 0.6f;

    private Rigidbody2D rb2D = null;
    private Animator animator = null;
    private float currentHealth;
    

    public Rigidbody2D Rb2D
    {
        get { return rb2D; }
        protected set { rb2D = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        protected set { currentHealth = value; }
    }

    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<Rigidbody2D>())
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }

        currentHealth = healthPool;
    }


    //On Death, if the character was an enemy, its script is disabled then the game object is destroyed.
    //If it is a player, the player script is disabled.
    protected virtual void Die()
    {
          
        animator.SetTrigger("Death");

        if (gameObject.GetComponent<Enemy>() != null)
        {
             gameObject.GetComponent<Enemy>().enabled = false;
             Destroy(gameObject, 2.2f);
        } else 
        {
            gameObject.GetComponent<Player>().enabled = false;
            
            
        }

       
        

    }

    

   
}
