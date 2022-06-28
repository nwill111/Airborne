using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageable
{

    //a WHOLE lot of fields
    [Header("Input")]
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode parryKey = KeyCode.Mouse1;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;
    public string xMoveAxis = "Horizontal";

    [Header("Movement")]
    public ParticleSystem doubleJumpDust;
    public Transform groundCheckCollider = null;
    private float activeMoveSpeed;
    public float dashSpeed;
    public bool playerMovement = true;

    public float dashLength = 0.5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    [Header("Combat")]
    public Transform attackOrigin = null;
    public float attackRadius = 0.5f;
    public float attackDamage = 5f;
    public float attackDelay = 0.5f;
    public float nextFireTime = 0f;
    public static int numberOfClicks = 0;
    public float lastClickedTime = 0f;
    public float maxComboDelay = 0.5f;
    public LayerMask enemyLayer = 8;
    public LayerMask groundLayer = 9;
    public LayerMask deathLayer = 11;
    public float groundCheckRadius = 0.1f;
    public float DamageDelay;
    public float movementSpeedWhileAttacking; 
    private bool invincibility = false;
    private bool isDead = false;

    public FlashEffect flash;
    public GameOver gameOver;

    [Header("Health")]
    public int upgradeLevel = 0;
    public int maxPlayerHealth;
    public HealthBar healthbar;

    [Header("Respawn")]
    private Vector3 respawnPoint;

    [Header("Audio")]
    public SoundManager soundManager;


    private float moveIntentionX = 0f;
    private bool jumpIntention = false;
    private bool attemptAttack = false;
    private bool attemptDash = false;
    private bool isAttacking = false;
    private bool doubleJumped = false;
    private float jumps = 0;

    //Set respawn points, moveSpeed, movespeed while attacking, max health, and current health on start
    void Start()
    {
        respawnPoint = transform.position;
        activeMoveSpeed = speed;
        movementSpeedWhileAttacking = activeMoveSpeed/2;
        maxPlayerHealth = 100 + (10 * upgradeLevel);
        CurrentHealth = maxPlayerHealth;

    }

    //Contains methods that need to be called every frame
    void Update()
    {
        GetInput();
        HandleJump();
        HandleAttack();
        HandleDash();
        HandleAnimations();
    }

    void FixedUpdate()
    {
        HandleRun();
    }

    //Used for debugging and visualizing aspects on the game... does nothing for gameplay.
    void OnDrawGizmosSelected()
    {
        if (attackOrigin != null)
        {
            Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
            Gizmos.DrawWireSphere(groundCheckCollider.position, groundCheckRadius);
        }
    }


    //Gets user input
    private void GetInput()
    {

        if (playerMovement)
        {
        moveIntentionX = Input.GetAxis(xMoveAxis);
        jumpIntention = Input.GetKeyDown(jumpKey);
        attemptAttack = Input.GetKeyDown(attackKey);
        attemptDash = Input.GetKeyDown(dashKey);
        } else {
            moveIntentionX = 0;
            jumpIntention = false;
            attemptAttack = false;
            attemptDash = false;
        }
    }

    //Code for running left and right
    private void HandleRun()
    {
        //Flip sprite
        if (moveIntentionX > 0 && transform.rotation.y != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            

        }
        else if (moveIntentionX < 0 && transform.rotation.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            
        }

        //Move
        Vector2 moveVelocity = new Vector2(moveIntentionX * activeMoveSpeed, Rb2D.velocity.y);
        Rb2D.velocity = moveVelocity;

        
    }

    //Code for the dash 
    private void HandleDash()
    {
        //If dash is attempted, the player is moving, and is not on cooldown
        if (moveIntentionX != 0 && attemptDash && dashCoolCounter <= 0)
        {

            //Make player invincible, set movespeed to dashspeed, set counter to length
            invincibility = true;
            activeMoveSpeed = dashSpeed;

            //Start counter
            dashCounter = dashLength;
        }

        //If the counter is not over...
        if (dashCounter > 0)
        {

            //Decrease counter
            dashCounter -= Time.deltaTime;

            //If the counter is over...
            if (dashCounter <= 0)
            {

                //Set speed back to normal and invinvibility to false
                invincibility = false;
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        
        //If the user is not attacking, set the speed to normal speed. This is needed for the slowing effect when swinging.
        } else if (!isAttacking)
        {
            activeMoveSpeed = speed;
        }

        if (dashCoolCounter > 0)
        {
            //Countdown cooldown
            dashCoolCounter -= Time.deltaTime;
        }
 
    }

    //Code for attacking system
    private void HandleAttack()
    {

        //If attack is pressed and not on cooldown
        if (attemptAttack && Time.time > nextFireTime)
        {

            //Set last click time
            lastClickedTime = Time.time;

            //Add click number
            numberOfClicks++;

            //Slow player
            activeMoveSpeed = movementSpeedWhileAttacking;

            //Set is attacking to true
            isAttacking = true;


            //On the first click...            
            if(numberOfClicks == 1)
            {

                //Play Attack1 animation
                Animator.SetBool("Attack1", true);
                Animator.Play("Attack1");
            
            }

            //Limit num of clicks to 3
            numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);

            //On the second click...
            if (numberOfClicks >= 2 && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                //Play attack2 animation
                Animator.SetBool("Attack1", false);
                Animator.SetBool("Attack2", true);
               
                
            }

            //On the third click...
            if (numberOfClicks >= 3 && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {

                //Play attack3 animation
                Animator.SetBool("Attack2", false);
                Animator.SetBool("Attack3", true);
                
            }
        } 

    }

    //Code for jumping
    private void HandleJump()
    {
        //If jump key and double jumped is not true
        if (jumpIntention && doubleJumped != true)
        {

            //Add jump velocity to x
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
            
        
            //If double jumped
            if (jumps > 0)
            {
                Animator.SetTrigger("doubleJump");
                createDust();
                doubleJumped = true;

            } 
            else
            {
                //Play jump sound
                soundManager.JumpSound();
            }
    
        }
        
        updateJump();
    }

    //Code to handle some of the animations
    private void HandleAnimations()
    {

        //Check grounded and set bool for animation map
        Animator.SetBool("Grounded", isGrounded());

        //If not attacking...
        if (!isAttacking)
        {
        //Set falling if velocity is below 0.25
        Animator.SetBool("isFalling", Rb2D.velocity.y < 0.25);
        }
        
        //Attack 1 animation
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            Animator.SetBool("Attack1", false);
      
        }

        //Attack 2 animation
         if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            Animator.SetBool("Attack2", false);
        
        }

        //Attack 3 animation
         if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3") && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            Animator.SetBool("Attack3", false);
            numberOfClicks = 0;
            isAttacking = false;
        }
       
         //Reset combo after some time 
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
            isAttacking = false;
        }
        

        //Dash
        if (moveIntentionX != 0 && attemptDash && dashCoolCounter <= 0)
        {
            Animator.SetTrigger("Dash");
        }

        //Move animation
        if (moveIntentionX != 0)
        {
            if (!isAttacking)
            {
                Animator.SetBool("isRunning", true);
            } 
        } else {
            if (!isAttacking)
            {
                Animator.SetBool("isRunning", false);
            } 
        }

        //Jump animation
        if (jumpIntention && doubleJumped != true) 
        {
            if (!isAttacking)
            {
                 Animator.SetTrigger("takeOff");
            }
        } 
    }


    //Check for ground below players feet
    private bool isGrounded()
    {
        //Check if the player is grounded by checking if the ground object connects with Ground Layer object colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);

        if (colliders.Length > 0)
        {
            return true;
        } else {
            return false;
        }

    }

    //Update jump counter for double jump
    private void updateJump()
    {
    
        if (isGrounded())
        {
            doubleJumped = false;
            jumps = 0;
            
        } else {
            jumps = jumps + 1;
        }
    
    }

    //Code for taking damage
    public virtual void TakeDamage(float damage)
    {
        //If not invincible and not dead...
        if (invincibility == false && isDead == false)
        {
            //Decrease health by damage, update healthbar, flash player, shake screen
            CurrentHealth -= damage;
            healthbar.UpdateHealth(CurrentHealth);
            soundManager.DamageSound();
            flash.StartFlash();
            ScreenShakeController.instance.StartShake(.15f, .045f);

            //If health is below 0...
            if (CurrentHealth <= 0)
            {
                //Disable player movement, Stop player movement, Show game over screen, Call Die function, set dead to true
                playerMovement = false;
                Rb2D.velocity = Vector2.zero;
                gameOver.GameOverScreen();
                Die();
                isDead = true;

            }

        }
    }

    //Completly heal player by setting health to maxhealth
    public void Heal()
    {
        CurrentHealth = maxPlayerHealth;
        healthbar.UpdateHealth(CurrentHealth);
    }

    //Play particle system below players feet for doublejumping
    private void createDust()
    {
        doubleJumpDust.Play();
    }

    //Deal damage to enemies
    private void dealDamage()
    {

        //Check objects in attack circle
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyLayer);

            //For every overlap, damage that object
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                IDamageable enemyAttributes = overlappedColliders[i].GetComponent<IDamageable>();
                if (enemyAttributes != null)
                {
                    //Play hit sound and damage
                    soundManager.HitSound();
                    enemyAttributes.TakeDamage(attackDamage);
                }
            }
    }

    //Respawn player
    public void Respawn()
    {
        //Set position to respawn point
        transform.position = respawnPoint;

        //Set health to max health (heal player)
        CurrentHealth = maxPlayerHealth;

        //Enable player 
        gameObject.GetComponent<Player>().enabled = true;

        //Enable movement
        playerMovement = true;

        //Set isDead to false
        isDead = false;

        //Update health bar
        healthbar.UpdateHealth(CurrentHealth);

        //Play idle animation
        Animator.Play("Idle");

        //Hide game over screen
        gameOver.HideGameOver();
    }

   
    //Handles player trigger entries
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //If player enters checkpoint, set spawnpoint
        if (other.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }

        //If player enters upgrade...
        else if (other.gameObject.tag == "upgrade")
        {

            //Play upgrade sound
            soundManager.UpgradeSound();

            //Add 1 to upgrade level
            upgradeLevel = upgradeLevel + 1;
            
            //Set max and current health
            maxPlayerHealth = 100 + (10 * upgradeLevel);
            CurrentHealth = CurrentHealth + 10;

            //Set new health to healthbar
            healthbar.SetMaxHealth(maxPlayerHealth);
            healthbar.UpdateHealth(CurrentHealth);

            
        }
    }

}
