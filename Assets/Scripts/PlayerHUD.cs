using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    // HUD Images for power system and weapon indicators
    public Image[] powerSystemImages; // Array of three images for the power system
    public Image meleeWeaponImage;    // Image for melee weapon indicator
    public Image rangedWeaponImage;   // Image for ranged weapon indicator

    private bool isMelee = false;

    // Update is called once per frame
    void Update()
    {
        // Detect key presses for power inputs
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdatePowerSystem(0); // Display first power system image
            UpdateWeaponIndicator(true); // Update to melee weapon
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            UpdatePowerSystem(1); // Display second power system image
            UpdateWeaponIndicator(false); // Update to ranged weapon
        }
    }

    // Update the power system display with the appropriate image
    public void UpdatePowerSystem(int index)
    {
        for (int i = 0; i < powerSystemImages.Length; i++)
        {
            powerSystemImages[i].enabled = (i == index); // Show only the active image
        }
    }

    // Update the weapon indicator images ("M" for melee, "R" for ranged)
    public void UpdateWeaponIndicator(bool melee)
    {
        isMelee = melee;

        meleeWeaponImage.enabled = isMelee;  // Show melee image
        rangedWeaponImage.enabled = !isMelee; // Show ranged image
    }
}
