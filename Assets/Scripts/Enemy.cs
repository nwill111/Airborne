using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IDamageable
{

    [Header("AI")]
    public float agroRange;
    public float minRange;
    public Transform target = null;
    public bool isFlipped = false;
    public float distanceToTarget;
    
    [Header("Combat")]
    public HitBoxHandler hitbox;
    public bool isDamageable = true;
    public FlashEffect flash;
    public float attackRange;
    public float timer;
    private float nextFireTime = 0.0f;
    public float attackCooldown;
    private bool isAttacking;
    private bool hitBoxToggle = false;
    private bool attackComplete = true;
    public EnemyHealthBar healthbar;


    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        

        //Get distance between enemy and player
        distanceToTarget = Vector2.Distance(transform.position, target.position);

        //If the distance is lower than the agro range AND the enemy isnt too close to the player AND the enemy isnt attacking...
        if (distanceToTarget <= agroRange && distanceToTarget >= minRange && !isAttacking)
        {
            //Chase the target
            ChaseTarget();
        } 
        else
        {
            //If not... stop the chase
            StopChase();
        }

        //If the distance is lower than attack range and attack is not on cooldown...
        if (distanceToTarget <= attackRange && Time.time > nextFireTime)
        {
            //Reset cooldown time and attack
            nextFireTime = Time.time + attackCooldown;
            Attack();
        } 
        else if (attackComplete)
        {
            //Stop attack once the attack is complete
            stopAttack();
        }

    }

    //Code for the enemy taking damage
    public virtual void TakeDamage(float damage)
    {
        if(isDamageable)
        {

            //Decrease the enemies current health by the damage done
            CurrentHealth -= damage;

            //Update the enemies healthbar
            healthbar.UpdateHealth(CurrentHealth);

            //Flash red
            flash.StartFlash();

            //Shake screen
            ScreenShakeController.instance.StartShake(.15f, .045f);

            //Once the enemies health is below 0 the enemy dies
            if (CurrentHealth <= 0)
            {
                isDamageable = false;
                Die();
            }
        } 
    }

    public void ChaseTarget()
    {
        Vector3 scale = transform.localScale;

        //Head towards the player
        if (transform.position.x < target.position.x)
        {
            scale.x = Mathf.Abs(scale.x);

            if (isFlipped)
            {
                scale.x = scale.x * -1;
            }

            Rb2D.velocity = new Vector2(speed, 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1;

            if (isFlipped)
            {
                scale.x = scale.x * -1;
            }

            Rb2D.velocity = new Vector2(-speed, 0);
        }

        transform.localScale = scale;
    }

    public void StopChase()
    {
        //Stops the chase by setting the enemies velocity to 0.
        Rb2D.velocity = Vector2.zero;
    }

    public void Attack()
    {
        //Trigger attack animation and set appropriate bools
        isAttacking = true;
        attackComplete = false;
        Animator.SetBool("Attack1", true);

        
    }

    public void stopAttack()
    {
        //Set Attack Bools to false to stop animations
        Animator.SetBool("Attack1", false);
        Animator.SetBool("Attack2", false);
        isAttacking = false;
    }

    //Toggles the hitbox for the enemies weapon
    public void toggleHitBox() {

        if (hitBoxToggle == false)
        {
            hitBoxToggle = true;
            hitbox.enableHitbox();
            
        }
        else
        {
            hitBoxToggle = false;
            hitbox.disableHitbox();
        }
    }

    //Used for combo of enemy weapons
    public void attack1Completed()
    {
        attackComplete = true;

        //If the player is still close to the enemy and attack 1 is done...
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f && distanceToTarget <= attackRange)
        {
            //Start attack 2
            Animator.SetBool("Attack2", true);
            attackComplete = false;

            //Reset cooldown
            nextFireTime = Time.time + attackCooldown;
        }
    }

    public void attack2Completed()
    {
        attackComplete = true;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if(other.gameObject.tag == "enemyBarrier")
        {
            StopChase();
        }
        
    }
 

    
}
