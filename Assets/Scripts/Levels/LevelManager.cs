using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> groundEnemies = new List<GameObject>();

    public GameObject doorOne;    // First door
    public GameObject doorTwo;    // Second door for the next area
    public GameObject enemyAreaOne;
    public GameObject enemyAreaTwo;

    private bool areaOneCleared = false;
    private bool isAreaTwoActivated = false;  // New flag for area two

    void Start()
    {
        UpdateGroundEnemies(enemyAreaOne);  // Start with the first area
        AddHealthListeners();               // Add health listeners for the first area
    }

    void Update()
    {
        EnemyCheck();
    }

    // Update the list of ground enemies in the given area
    private void UpdateGroundEnemies(GameObject enemyArea)
    {
        groundEnemies.Clear();  // Clear the current list of enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("GroundEnemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.transform.IsChildOf(enemyArea.transform))  // Only enemies in the current area
            {
                groundEnemies.Add(enemy);
            }
        }
    }

    // Check if all ground enemies have been destroyed
    public void EnemyCheck()
    {
        groundEnemies.RemoveAll(enemy => enemy == null || !enemy);

        if (groundEnemies.Count == 0)
        {
            if (!areaOneCleared)
            {
                // First area cleared, open first door and activate second area
                areaOneCleared = true;
                OpenDoor(doorOne, "isAreaOneCleared");

                // Activate second area and delay the update of ground enemies
                enemyAreaTwo.SetActive(true);
                Debug.Log("The First Area is Over");
                StartCoroutine(DelayedActivateSecondArea());
            }
            else if (isAreaTwoActivated)  // Only check if area two has been activated
            {
                Debug.Log("The Second Area is Over");
                // Second area cleared, open second door (final escape)
                OpenDoor(doorTwo, "isAreaTwoCleared");
            }
        }
    }

    // Coroutine to delay the enemy update for the second area
    private IEnumerator DelayedActivateSecondArea()
    {
        yield return new WaitForSeconds(1.0f);  // Delay to ensure enemies are fully activated
        UpdateGroundEnemies(enemyAreaTwo);  // Update with enemies from the second area
        AddHealthListeners();               // Add health listeners for the second area's enemies
        isAreaTwoActivated = true;          // Mark area two as activated
    }

    // Remove a specific enemy from the list when they die
    public void RemoveEnemy(GameObject enemy)
    {
        if (groundEnemies.Contains(enemy))
        {
            groundEnemies.Remove(enemy);
            EnemyCheck();
        }
    }

    // Add listeners to each enemy's health component
    private void AddHealthListeners()
    {
        foreach (GameObject enemy in groundEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.outOfHealth.AddListener(() => RemoveEnemy(enemy));
            }
        }
    }

    // Open the door using its own Animator and a specific parameter
    private void OpenDoor(GameObject door, string animatorParameter)
    {
        if (door != null)
        {
            Animator doorAnim = door.GetComponent<Animator>();
            if (doorAnim != null)
            {
                doorAnim.SetBool(animatorParameter, true);
                Debug.Log("Opened " + door.name + " with parameter: " + animatorParameter);
            }
            else
            {
                Debug.Log("Animator component is missing on " + door.name);
            }
        }
        else
        {
            Debug.Log("Door reference is not assigned.");
        }
    }
}
