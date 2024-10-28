using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject hud; // The main HUD object
    public GameObject dashLines; // Reference to the DashLines

    void Start()
    {
        HideHUD(); // Hide HUD at the start
    }

    public void ShowHUD()
    {
        hud.SetActive(true); // Show HUD
        dashLines.SetActive(true); // Ensure DashLines are enabled
    }

    public void HideHUD()
    {
        hud.SetActive(false); // Hide HUD
        dashLines.SetActive(false); // Disable DashLines if needed
    }
}
