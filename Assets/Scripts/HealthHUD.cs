using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper;

public class HealthHUD : MonoBehaviour
{
    public Health playerHealth; // Reference to the player's health script
    public Text healthText; // Reference to the UI Text element

    void Start()
    {
        if (playerHealth != null)
        {
            // Subscribe to the health changed event
            playerHealth.healthChanged.AddListener(OnHealthChanged);
            // Initialize the health text
            UpdateHealthText(playerHealth.currentHealth, playerHealth.maxHealth);
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
        // Update the health text when the health changes
        UpdateHealthText(healthChangedObject.currentHealth, healthChangedObject.maxHealth);
    }

    void UpdateHealthText(int currentHealth, int maxHealth)
    {
        // Update the UI text to display current and max health
        healthText.text = $"Health: {currentHealth} / {maxHealth}";
    }
}
