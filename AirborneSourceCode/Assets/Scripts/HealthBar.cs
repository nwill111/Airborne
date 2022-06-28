using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Code for Player Healthbar
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //Update the healthbar value
    public void UpdateHealth(float health)
    {
        slider.value = health;
    }   

    //Set the max value for the healthbar
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}
