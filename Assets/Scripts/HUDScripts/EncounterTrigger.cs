using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    private HUDManager hudManager;
    private ThirdPersonMovement playerMovement;

    public int enemyCount;

    private void Awake()
    {
        hudManager = FindObjectOfType<HUDManager>();
        playerMovement = FindObjectOfType<ThirdPersonMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudManager.ShowHUD();

            // Reset DashLines visibility when entering combat area if currently dashing
            if (playerMovement.dashing) 
            {
                playerMovement.dashLines.SetActive(false);
                playerMovement.dashing = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudManager.HideHUD();
        }
    }

    public void EndEncounter()
    {
        Debug.Log("Encounter ended, hide HUD.");
        hudManager.HideHUD();
    }
}
