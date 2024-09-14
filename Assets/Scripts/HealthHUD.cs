using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper;

public class HealthHUD : MonoBehaviour
{
    public Health playerHealth; // Reference to the player's health script
    public Image healthBar; // Reference to the health bar UI image

    void Start()
    {
        if (playerHealth != null)
        {
            // Subscribe to the health changed event
            playerHealth.healthChanged.AddListener(OnHealthChanged);
            // Initialize the health bar
            UpdateHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);
        }
        else
        {
            Debug.LogError("Player Health is not assigned in the HealthHUD script.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the health changed event when this object is destroyed
        if (playerHealth != null)
        {
            playerHealth.healthChanged.RemoveListener(OnHealthChanged);
        }
    }

    void OnHealthChanged(HealthChangedObject healthChangedObject)
    {
        // Update the health bar when the health changes
        UpdateHealthBar(healthChangedObject.currentHealth, healthChangedObject.maxHealth);
    }

    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        // Update the fill amount of the health bar image
        float fillAmount = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = fillAmount;
    }
}
