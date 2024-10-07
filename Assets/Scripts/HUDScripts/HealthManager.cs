using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SuperPupSystems.Helper;

public class HealthManager : MonoBehaviour
{
    public Health playerHealth; // Reference to the player's health script
    public Slider healthSlider; // Reference to the health bar UI slider
    public Image healthFillImage; // Reference to the fill image of the health bar

    private bool isFlashing = false; // To check if the health bar is currently flashing
    public Color healthBarNormalColor = Color.red; // The normal color for the health bar (red)
    public Color flashColor = Color.white; // The color the health bar flashes (white)
    public float flashDuration = 0.1f; // Duration of each flash
    public float flashRepeatDelay = 0.2f; // Delay between flashes

    void Start()
    {
        // Initialize the health slider with max health
        healthSlider.maxValue = playerHealth.maxHealth;
        SetHealth(playerHealth.currentHealth);

        // Subscribe to the healthChanged event
        playerHealth.healthChanged.AddListener(OnHealthChanged);
    }

    // This method will be called when the player's health changes
    public void OnHealthChanged(HealthChangedObject healthObj)
    {
        SetHealth(healthObj.currentHealth);
    }

    // Updates the health bar based on current health
    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
        //Debug.Log("Health updated: " + currentHealth); // Debug log for checking health updates
        
        // Check if the health is below 25%, start flashing if necessary
        if (currentHealth / (float)playerHealth.maxHealth < 0.35f && !isFlashing)
        {
            //Debug.Log("Health is below 25%, starting flash..."); // Debug log for flashing condition
            StartCoroutine(FlashHealthBar());
        }
        else if (currentHealth / (float)playerHealth.maxHealth >= 0.35f && isFlashing)
        {
            StopCoroutine(FlashHealthBar()); // Stop flashing when health is above 35%
            isFlashing = false;
            healthFillImage.color = healthBarNormalColor; // Reset back to the normal color (red)
        }
    }

    // Coroutine to flash the health bar
    private IEnumerator FlashHealthBar()
    {
        isFlashing = true; // Mark that flashing has started

        while (playerHealth.currentHealth / (float)playerHealth.maxHealth < 0.35f)
        {
            // Flash to white
            healthFillImage.color = flashColor;
            healthFillImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 1f); // Ensure alpha is fully opaque
            yield return new WaitForSeconds(flashDuration);

            // Flash back to red
            healthFillImage.color = healthBarNormalColor;
            healthFillImage.color = new Color(healthBarNormalColor.r, healthBarNormalColor.g, healthBarNormalColor.b, 1f); // Ensure alpha is fully opaque
            yield return new WaitForSeconds(flashRepeatDelay);
        }

        isFlashing = false; // Stop flashing once health is above 35%
    }
}