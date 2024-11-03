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
    }

    public void HideHUD()
    {
        hud.SetActive(false); // Hide HUD
    }

    public void ShowDashLines()
    {
        dashLines.SetActive(true); // Show DashLines
    }

    public void HideDashLines()
    {
        dashLines.SetActive(false); // Hide DashLines
    }
}
