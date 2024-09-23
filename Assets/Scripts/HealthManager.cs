using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper; // If this is a custom namespace you're using

public class HealthManager : MonoBehaviour
{
    public Health playerHealth; // Reference to the player's health script
    public Slider healthSlider; // Reference to the health bar UI slider

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth != null && healthSlider != null)
        {
            // Initialize the slider's values at the start
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
        }
        else
        {
            if (playerHealth == null)
                Debug.LogError("Player Health is not assigned in the HealthManager script.");
            if (healthSlider == null)
                Debug.LogError("Health Slider is not assigned in the HealthManager script.");
        }
    }

    // This method can be called whenever health changes
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (healthSlider != null)
        {
            // Ensure the slider's max value matches the player's max health
            healthSlider.maxValue = maxHealth;

            // Update the slider's value to reflect current health
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("Health Slider is not assigned in the HealthManager script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
        {
            // Continuously update the health bar to reflect the player's health
            UpdateHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);
        }
    }
}
