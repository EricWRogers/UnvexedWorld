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

    void Start()
    {
        UpdateGroundEnemies(enemyAreaOne);  // Start with the first area
        AddHealthListeners();
    }

    void Update()
    {
        EnemyCheck();
    }

    // Update the list of ground enemies in the given area
    private void UpdateGroundEnemies(GameObject enemyArea)
    {
        groundEnemies.Clear();
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
                enemyAreaTwo.SetActive(true);  // Activate second area
                UpdateGroundEnemies(enemyAreaTwo);  // Update with new enemies
            }
            else
            {
                // Second area cleared, open second door (final escape)
                OpenDoor(doorTwo, "isAreaTwoCleared");
            }
        }
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
            }
            else
            {
                Debug.Log("Animator component is missing on the door.");
            }
        }
        else
        {
            Debug.Log("Door reference is not assigned.");
        }
    }
}
