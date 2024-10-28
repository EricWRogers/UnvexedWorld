using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    private HUDManager hudManager;

    private void Start()
    {
        hudManager = FindObjectOfType<HUDManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudManager.ShowHUD(); // Show HUD when entering the encounter area
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudManager.HideHUD(); // Hide HUD when exiting the encounter area
        }
    }
}
