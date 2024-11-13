using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class ActivateFight : MonoBehaviour
{
    public GameObject fogArea; // Optional fog area if needed
    private bool on = false;

    [SerializeField]
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private Collider fightAreaCollider;
    private HUDManager hudManager;
    private AudioManager audioManager;

    private int totalEnemies; // Track total number of enemies
    private int defeatedEnemies; // Track number of defeated enemies

    private void Start()
    {
        // Initialize components
        fightAreaCollider = GetComponent<Collider>();
        hudManager = FindObjectOfType<HUDManager>();
        audioManager = FindObjectOfType<AudioManager>();

        // Add all child enemies to enemiesInZone list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemy")) // Ensure only enemies are added
            {
                enemiesInZone.Add(child.gameObject);
            }
        }

        totalEnemies = enemiesInZone.Count; // Set the total number of enemies in the zone
        defeatedEnemies = 0; // Initialize defeated enemies count

        // Ensure background music starts if AudioManager is available
        audioManager?.PlayBackgroundMusic();

        // Add health listeners to each enemy to track defeat
        foreach (var enemy in enemiesInZone)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                // Add listener to detect when enemy's health reaches zero
                enemyHealth.outOfHealth.AddListener(() => OnEnemyDefeated(enemy));
            }
        }
    }

    private void FixedUpdate()
    {
        if(on && transform.childCount == 1)
        {
            ActivateFight[] argoZone = FindObjectsOfType<ActivateFight>();

            int count = 0;

            foreach(ActivateFight ae in argoZone)
            {
                if(ae.on == true)
                {
                    count++;
                }
            }

           

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !on)
        {
            on = true;
            fogArea?.SetActive(true); // Activate fog area if specified
            hudManager.ShowHUD();

            // Start battle music when entering
            audioManager?.PlayBattleMusic();

            foreach (var enemy in enemiesInZone)
            {
                if (enemy.TryGetComponent<MeleeStateMachine>(out var meleeStateMachine))
                {
                    meleeStateMachine.isInsideCollider = true;
                }
                else if (enemy.TryGetComponent<AgroMeleeStateMachine>(out var agroMeleeStateMachine))
                {
                    agroMeleeStateMachine.isInsideCollider = true;
                }
                else if (enemy.TryGetComponent<RangeStateMachine>(out var rangeStateMachine))
                {
                    rangeStateMachine.isInsideCollider = true;
                }
            }

            // Disable the collider after player enters to avoid re-triggering
            if (fightAreaCollider != null)
            {
                fightAreaCollider.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && on)
        {
            on = false;
            fogArea?.SetActive(false); // Deactivate fog area
            hudManager.HideHUD();

            // Return to background music after exiting fight area
            if (!audioManager.IsBattleMusicPlaying())
            {
                audioManager?.PlayBackgroundMusic();
            }
        }
    }

    // Called when an enemy is defeated
    private void OnEnemyDefeated(GameObject enemy)
    {
        defeatedEnemies++;

        // Check if all enemies are defeated
        if (defeatedEnemies >= totalEnemies)
        {
            // Switch to background music when all enemies are defeated
            audioManager?.PlayBackgroundMusic();
        }
    }
}
