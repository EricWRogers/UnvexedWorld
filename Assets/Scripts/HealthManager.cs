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
    void Start()
    {
        if (playerHealth != null && healthSlider != null)
        {
            // Initialize the health slider at the start
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
        }
        else
        {
            Debug.LogError("Player Health or Health Slider is not assigned in the HealthManager script.");
        }
    }

    // Update the health slider when health changes
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        // Ensure the slider's max value matches the health system
        healthSlider.maxValue = maxHealth;

        // Update the slider's value to represent current health
        healthSlider.value = currentHealth;
    }
}
