using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper;

public class HealthManager : MonoBehaviour
{
    public Health playerHealth; // Reference to the player's health script
    public Image healthBar; // Reference to the health bar UI image

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth != null)
        {
            // Initialize the health bar at the start
            UpdateHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);
        }
        else
        {
            Debug.LogError("Player Health is not assigned in the HealthManager script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Health bar is updated when health changes, no need for constant update in this script
    }

    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        // Update the health bar fill amount
        float fillAmount = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = fillAmount;
    }
}
