using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public int comboCount = 0;  // Current combo count
    public float comboDuration = 2f;  // Time allowed between hits to maintain the combo
    private float comboTimer = 0f;

    public Slider comboSlider;  // Slider to represent combo bar (if you want to keep it)
    public TMP_Text comboCountText;  // Text to display combo count above the bar
    public TMP_Text comboMultiplierText;  // Text to display combo multiplier beside the bar

    // Multiplier thresholds
    private const int multiplier1Threshold = 5;  // Threshold for x2
    private const int multiplier2Threshold = 10; // Threshold for x3
    private const int multiplier3Threshold = 20; // Threshold for x4
    private const int multiplier4Threshold = 30; // Threshold for x5

    void Start()
    {
        comboSlider.maxValue = multiplier4Threshold;  // Set slider max value, can still be used for feedback
        comboSlider.value = 0;  // Initialize slider value
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
        comboCount++;  // Increase the combo count
        comboTimer = comboDuration;  // Reset combo timer
        UpdateComboUI();  // Update the combo meter UI
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

        // Update Slider value (using the last threshold as a reference for visual purposes)
        if (comboSlider != null)
        {
            comboSlider.value = Mathf.Clamp(comboCount, 0, multiplier4Threshold);  // Keep the slider value within the last multiplier
        }

        // Update the combo count text to just show the current combo count
        comboCountText.text = "Combo: " + comboCount;

        // Update the multiplier text
        UpdateComboMultiplier();
    }

    void UpdateComboMultiplier()
    {
        if (comboCount >= multiplier4Threshold)
        {
            comboMultiplierText.text = "x5";
        }
        else if (comboCount >= multiplier3Threshold)
        {
            comboMultiplierText.text = "x4";
        }
        else if (comboCount >= multiplier2Threshold)
        {
            comboMultiplierText.text = "x3";
        }
        else if (comboCount >= multiplier1Threshold)
        {
            comboMultiplierText.text = "x2";
        }
        else
        {
            comboMultiplierText.text = "x1";  // Default for lower combos
        }
    }

    // Decrease the combo only when the player gets hit
    public void PlayerHit()
    {
        ResetCombo();  // Reset combo when player gets hit
    }
}
