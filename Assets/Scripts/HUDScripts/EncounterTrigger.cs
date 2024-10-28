using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    private HUDManager hudManager;

    void Start()
    {
        // Reference the HUDManager component attached to your HUD GameObject
        hudManager = FindObjectOfType<HUDManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudManager.ShowHUD(); // No arguments needed
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudManager.HideHUD(); // No arguments needed
        }
    }
}
