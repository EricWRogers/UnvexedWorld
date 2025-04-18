using UnityEngine;

public class HUDManager : MonoBehaviour
{
    //public GameObject hud; // The main HUD object
    public GameObject dashLines; // Reference to the DashLines
    public GameObject radialMenu;

    public bool stop =  false;

    void Start()
    {
        radialMenu.SetActive(true);
    }

    /*public void ShowHUD()
    {
        hud.SetActive(true); // Show HUD
        stop = true;
    }*/

    /*public void HideHUD()
    {
        hud.SetActive(false); // Hide HUD
        stop = false;
    }*/

    public void ShowDashLines()
    {
        dashLines.SetActive(true); // Show DashLines
    }

    public void HideDashLines()
    {
        dashLines.SetActive(false); // Hide DashLines
    }
}
