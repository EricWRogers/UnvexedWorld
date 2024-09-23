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
    public void Start()
    {
        healthSlider.maxValue = playerHealth.maxHealth;
        SetHealth();
    }

    public void SetHealth()
    {
        healthSlider.value = playerHealth.currentHealth;
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
