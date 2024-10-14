using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public int comboCount = 0;  // Current combo count
    public float comboDuration = 2f;  // Time allowed between hits to maintain the combo
    private float comboTimer = 0f;

    public int maxCombo = 100;  // Maximum combo limit (out of 100)
    public Slider comboSlider;  // Slider to represent combo bar
    public TMP_Text comboCountText;  // Text to display combo count above the bar
    public TMP_Text comboMultiplierText;  // Text to display combo multiplier beside the bar

    // Multiplier thresholds
    private const float multiplier1Threshold = 0.25f;  // 25% combo for x2
    private const float multiplier2Threshold = 0.50f;  // 50% combo for x3
    private const float multiplier3Threshold = 0.75f;  // 75% combo for x4
    private const float multiplier4Threshold = 1.0f;   // 100% combo for x5

    void Start()
    {
        comboSlider.maxValue = maxCombo;  // Set max value for slider
        comboSlider.value = comboCount;  // Initialize slider value
    }

    void Update()
    {
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                ResetCombo();  // Reset combo if the timer runs out
            }
        }
    }

    public void IncrementCombo()
    {
        if (comboCount < maxCombo)
        {
            comboCount++;  // Increase the combo count
            comboTimer = comboDuration;  // Reset combo timer
            UpdateComboUI();  // Update the combo meter UI
        }
    }

    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboUI();  // Update the UI after resetting
    }

    void UpdateComboUI()
    {
        // Debug the combo count to check the updates
        Debug.Log("Updating Combo UI. Combo Count: " + comboCount);

        // Update Slider value
        if (comboSlider != null)
        {
            comboSlider.value = comboCount;  // Update slider value
            Debug.Log("Slider Value: " + comboSlider.value);
        }
        else
        {
            Debug.LogWarning("Combo Slider is not assigned!");
        }

        // Update the combo count text
        comboCountText.text = comboCount + " / " + maxCombo;

        // Update the multiplier text
        UpdateComboMultiplier();

        if (comboCount <= 0)
        {
            comboCountText.text = "Combo: 0";  // Optionally hide text if combo is 0
        }
    }

    void UpdateComboMultiplier()
    {
        float comboPercentage = (float)comboCount / maxCombo;

        if (comboPercentage >= multiplier4Threshold)
        {
            comboMultiplierText.text = "x5";
        }
        else if (comboPercentage >= multiplier3Threshold)
        {
            comboMultiplierText.text = "x4";
        }
        else if (comboPercentage >= multiplier2Threshold)
        {
            comboMultiplierText.text = "x3";
        }
        else if (comboPercentage >= multiplier1Threshold)
        {
            comboMultiplierText.text = "x2";
        }
        else
        {
            comboMultiplierText.text = "x1";  // Default if below 25%
        }
    }

    // Decrease the combo only when the player gets hit
    public void PlayerHit()
    {
        ResetCombo();  // Reset combo when player gets hit
    }
}
