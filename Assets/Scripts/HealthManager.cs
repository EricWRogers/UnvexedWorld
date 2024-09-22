using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper;

public class HealthManager : MonoBehaviour
{
    public Health playerHealth; // Reference to the player's health script
    public Slider healthSlider; // Reference to the health bar UI slider

    // Start is called before the first frame update
    public void Start()
    {
        healthSlider.maxValue = playerHealth.maxHealth;
        SetHealth();
    }

    public void SetHealth()
    {
        healthSlider.value = playerHealth.currentHealth;
    }
}
