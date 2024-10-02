using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHUD : MonoBehaviour
{
    // HUD Images for power system and weapon indicators
    public Image[] powerSystemImages; // Array of three images for the power system
    public Image meleeWeaponImage;    // Image for melee weapon indicator
    public Image rangedWeaponImage;   // Image for ranged weapon indicator

    public GameObject spellReady;

    public SpellCraft spellCraft;

    public List<Color> colors = new List<Color>();

    // Colors for key indicators
    
    private Color defaultColor = Color.white; // Default color for empty slots
    private Color highlightColor = Color.yellow; // Color for highlighting selected weapons
    private Color green = Color.green; // Color for combining spells
    private Color purple = new Color(0.5f, 0, 0.5f); // Purple for reset

    void Start()
    {
        colors.Add(Color.clear);
        colors.Add(Color.blue);
        colors.Add(Color.red);
        colors.Add(Color.yellow);
        spellReady.SetActive(false);
        spellCraft = gameObject.GetComponentInParent<SpellCraft>();
    }

    // Update is called once per frame
    void Update()
    {
        powerSystemImages[0].color = colors[(int)spellCraft.mainAspect];
        powerSystemImages[1].color = colors[(int)spellCraft.modAspect];
        // Detect key presses for power inputs
        // if (spellCraft.mainAspect == SpellCraft.Aspect.scavenge) // Key '1' pressed for blue spell
        // {
        //     SetNextAvailablePowerBoxColor(blue); // Fill the next box with blue
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha2)) // Key '2' pressed for red spell
        // {
        //     SetNextAvailablePowerBoxColor(red); // Fill the next box with red
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha4)) // Key '4' pressed to reset
        // {
        //     ResetPowerSystem(); // Reset the power system
        // }


        // Check if F key is held down for combining spells
        
        spellReady.SetActive(spellCraft.casting); // Turn on the Game Object to let you know spell is ready

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
