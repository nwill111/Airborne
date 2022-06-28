using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Vector3 offset;
    public Slider slider;

    //Set enemy healthbar max health
    public void Awake()
    {
        SetMaxHealth(10);
    }

    //Function for updating the enemy health bar
    public void UpdateHealth(float health)
    {

        //Show health bar if enemy is hurt
        slider.gameObject.SetActive(health < 10);
        slider.value = health;

        //If enemy is dead, hide the bar
        if (health <= 0)
        {
            slider.gameObject.SetActive(false);
        }
    }   

    //Function for setting the max health for the health bar
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    //Move the health bar with the enemy every frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
