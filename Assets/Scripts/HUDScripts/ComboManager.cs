using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    public int comboCount = 0;  // Current combo count
    public float comboDuration = 2f;  // Time allowed between hits to maintain the combo
    private float comboTimer = 0f;

    // Slider for the combo meter
    public Slider comboSlider; // One slider for all grades
    //public Image comboFill;
    public int tier = 0;

    public TMP_Text comboCountText;  // Text to display combo count
    public TMP_Text multiplierText;   // Text to display multiplier
    public RectTransform comboMeterTransform;  // For shaking effect

    // Grading thresholds
    private const int gradeCThreshold = 25;
    private const int gradeBThreshold = 50;
    private const int gradeAThreshold = 75;
    private const int gradeSThreshold = 100;

    // Grade images
    public Image gradeCImage; // For displaying grade C
    public Image gradeBImage; // For displaying grade B
    public Image gradeAImage; // For displaying grade A
    public Image gradeSImage; // For displaying grade S

    void Start()
    {
        // Hide all grade images initially
        gradeCImage.gameObject.SetActive(false);
        gradeBImage.gameObject.SetActive(false);
        gradeAImage.gameObject.SetActive(false);
        gradeSImage.gameObject.SetActive(false);

        ResetCombo();  // Initialize UI and deactivate all but C
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
        comboCount += 3;  // Increase the combo count
        comboTimer = comboDuration;  // Reset combo timer
        UpdateComboUI();  // Update the combo meter UI
    }

    public void ResetCombo()
    {
        tier = 0;
        comboCount = 0; // Reset to 0
        //ResetComboUI();  // Reset UI after resetting
    }

    void ResetComboUI()
    {
        // Update the combo count text
        comboCountText.text = "Combo: " + comboCount;
        //comboSlider.value = 0;  // Reset the slider
        int multiplier = CalculateMultiplier(comboCount);
        multiplierText.text = "x1";  // Reset multiplier text

        // Reset grade images
        gradeCImage.gameObject.SetActive(true);
        gradeBImage.gameObject.SetActive(false);
        gradeAImage.gameObject.SetActive(false);
        gradeSImage.gameObject.SetActive(false);

        UpdateComboUI();
    }

    void UpdateComboUI()
    {
        // Update the combo count text
        comboCountText.text = "Combo: " + comboCount;

        // Update the multiplier based on the current combo count
        int multiplier = CalculateMultiplier(comboCount);
        multiplierText.text = "x" + multiplier;

        gradeCImage.gameObject.SetActive(false);
        gradeBImage.gameObject.SetActive(false);
        gradeAImage.gameObject.SetActive(false);
        gradeSImage.gameObject.SetActive(false);
        //UpdateSlider(25);
        //comboFill.color = Color.grey;

        switch (multiplier)
        {
            case 2:
                gradeCImage.gameObject.SetActive(true);
                //comboFill.color = Color.white;
                break;
            case 3:
                gradeBImage.gameObject.SetActive(true);
                //comboFill.color = Color.blue;
                break;
            case 4:
                gradeAImage.gameObject.SetActive(true);
                //comboFill.color = Color.green;
                break;
            case 5:
                gradeSImage.gameObject.SetActive(true);
                //comboFill.color = Color.red;
                //comboSlider.value = comboSlider.maxValue;
                break;
        }

        // Determine current grade based on combo count
        if (CalculateMultiplier(comboCount) == 2)
        {
            // Grade C
            gradeCImage.gameObject.SetActive(true);
            gradeBImage.gameObject.SetActive(false);
            gradeAImage.gameObject.SetActive(false);
            gradeSImage.gameObject.SetActive(false);
            //UpdateSlider(25);
            //comboFill.color = Color.white;
        }
        else if (CalculateMultiplier(comboCount) == 3)
        {
            // Grade B
            gradeCImage.gameObject.SetActive(false);
            gradeBImage.gameObject.SetActive(true);
            gradeAImage.gameObject.SetActive(false);
            gradeSImage.gameObject.SetActive(false);
            //UpdateSlider(25);
            //comboFill.color = Color.blue;
        }
        else if (CalculateMultiplier(comboCount) == 4)
        {
            // Grade A
            gradeCImage.gameObject.SetActive(false);
            gradeBImage.gameObject.SetActive(false);
            gradeAImage.gameObject.SetActive(true);
            gradeSImage.gameObject.SetActive(false);
            //UpdateSlider(25);
            //comboFill.color = Color.green;
        }
        else if (CalculateMultiplier(comboCount) == 5)
        {
            // Grade S
            gradeCImage.gameObject.SetActive(false);
            gradeBImage.gameObject.SetActive(false);
            gradeAImage.gameObject.SetActive(false);
            gradeSImage.gameObject.SetActive(true);
            //UpdateSlider(25);
            //comboFill.color = Color.red;
        }
    }

    void UpdateSlider(int threshold)
    {
        // Prevent S tier stacking
        if (tier >= 4)
        {
            //comboSlider.value = comboSlider.maxValue;  // Set the slider to the current combo count
            return;
        }

        //comboSlider.value = comboCount;

        // Check if we've reached a threshold
        if (comboCount >= threshold)
        {
            // Reset slider value for the next grade
            tier++;
            comboCount -= threshold;  // Move to next grade
            //comboSlider.value = 0;  // Reset slider value for new grade
        }
    }

    int CalculateMultiplier(int comboCount)
    {
        if (comboCount + (tier * 25) >= 100) return 5;
        if (comboCount + (tier * 25) >= 75) return 4;
        if (comboCount + (tier * 25) >= 50) return 3;
        if (comboCount + (tier * 25) >= 25) return 2;
        return 1; // Default multiplier is 1
    }

    // Reset combo when player gets hit
    public void PlayerHit()
    {
        if (tier > 0)
        {
            tier--; // Drop a letter grade
            comboCount = 25; // Reset combo count to the start of the current tier
        }
        else
        {
            comboCount = 0; // Reset to 0 if at the lowest tier
        }
        
        RefreshGradeUI(); // Call RefreshGradeUI to update visuals on hit
    }

    // Helper method to update grade visuals based on current tier
    private void RefreshGradeUI()
    {
        // Hide all grade images initially
        gradeCImage.gameObject.SetActive(false);
        gradeBImage.gameObject.SetActive(false);
        gradeAImage.gameObject.SetActive(false);
        gradeSImage.gameObject.SetActive(false);
        
        switch (tier)
        {
            case 1:
                gradeCImage.gameObject.SetActive(true);
                //comboFill.color = Color.white;
                break;
            case 2:
                gradeBImage.gameObject.SetActive(true);
                //comboFill.color = Color.blue;
                break;
            case 3:
                gradeAImage.gameObject.SetActive(true);
               //comboFill.color = Color.green;
                break;
            case 4:
                gradeSImage.gameObject.SetActive(true);
                //comboFill.color = Color.red;
                //comboSlider.value = comboSlider.maxValue;
                break;
        }
        //comboSlider.value = comboCount;
    }
}
