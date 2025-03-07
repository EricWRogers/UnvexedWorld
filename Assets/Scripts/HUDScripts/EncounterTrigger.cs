using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    private HUDManager hudManager;
    private AudioManager audioManager;

    private void Start()
    {
        // Initialize HUDManager and AudioManager references
        hudManager = FindFirstObjectByType<HUDManager>();
        audioManager = FindFirstObjectByType<AudioManager>();

        if (hudManager == null)
        {
            Debug.LogWarning("HUDManager not found in the scene.");
        }

        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hudManager?.ShowHUD();
            audioManager?.PlayBattleMusic(); // Start battle music on entering
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hudManager?.HideHUD();
            audioManager?.PlayBackgroundMusic(); // Switch back to background music
        }
    }
}
