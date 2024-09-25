using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour
{
    // HUD Images for power system and weapon indicators
    public Image[] powerSystemImages; // Array of three images for the power system
    public Image meleeWeaponImage;    // Image for melee weapon indicator
    public Image rangedWeaponImage;   // Image for ranged weapon indicator

    public GameObject spellReady;

    private int currentIndex = 0; // Tracks which power box to fill next
    private bool isMelee = false;

    // Colors for key indicators
    private Color blue = Color.blue;
    private Color red = Color.red;
    private Color defaultColor = Color.white; // Default color for empty slots
    private Color highlightColor = Color.yellow; // Color for highlighting selected weapons
    private Color green = Color.green; // Color for combining spells
    private Color purple = new Color(0.5f, 0, 0.5f); // Purple for reset

    void Start()
    {
        spellReady.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Detect key presses for power inputs
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Key '1' pressed for blue spell
        {
            SetNextAvailablePowerBoxColor(blue); // Fill the next box with blue
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Key '2' pressed for red spell
        {
            SetNextAvailablePowerBoxColor(red); // Fill the next box with red
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // Key '4' pressed to reset
        {
            ResetPowerSystem(); // Reset the power system
        }

        // Check if F key is held down for combining spells
        if (Input.GetKey(KeyCode.F))
        {
            spellReady.SetActive(true); // Turn on the Game Object to let you know spell is ready
        }
        else if (Input.GetKeyUp(KeyCode.F)) // Return to default color when F is released
        {
            spellReady.SetActive(false);
        }

        // Detect mouse clicks for melee and ranged weapon highlights
        if (Input.GetMouseButtonDown(0)) // Left click for melee
        {
            HighlightMeleeWeapon(); 
        }
        else if (Input.GetMouseButtonDown(1)) // Right click for ranged
        {
            HighlightRangedWeapon(); 
        }
    }

    // Method to fill the next available box with heading spell color (either blue or red)
    private void SetNextAvailablePowerBoxColor(Color color)
    {
        if (currentIndex < powerSystemImages.Length) // Only fill first two boxes
        {
            powerSystemImages[currentIndex].color = color;
            currentIndex++; // Move to the next box
        }
    }

    // Method to reset the power system and show the reset state in the third box
    private void ResetPowerSystem()
    {
        // Reset the first two boxes to default color
        for (int i = 0; i < Mathf.Min(2, powerSystemImages.Length); i++)
        {
            Color resetColor = defaultColor; // Start with default color
            resetColor.a = 0f; // Set alpha to 0
            powerSystemImages[i].color = resetColor; // Apply the color with zero alpha
        }
        currentIndex = 0; // Reset index to fill from the beginning again
    }

    // Highlight the melee weapon box
    private void HighlightMeleeWeapon()
    {
        meleeWeaponImage.color = highlightColor; // Highlight melee weapon with yellow
        rangedWeaponImage.color = defaultColor;  // Reset ranged weapon to default
    }

    // Highlight the ranged weapon box
    private void HighlightRangedWeapon()
    {
        rangedWeaponImage.color = highlightColor; // Highlight ranged weapon with yellow
        meleeWeaponImage.color = defaultColor;    // Reset melee weapon to default
    }
}
