using System.Collections.Generic;
using UnityEngine;

public class LeaveLevel : MonoBehaviour
{
    // Store references to all ground enemies
    private List<GameObject> groundEnemies = new List<GameObject>();
    private Animator anim;

    // Reference to the door GameObject
    public GameObject door;
    public GameObject enemyArea;
    public GameObject enemyAreaTwo;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // Optionally, you can initialize groundEnemies here if needed
        UpdateGroundEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all ground enemies have been destroyed
        EnemyCheck();
    }

    // Update the list of ground enemies
    private void UpdateGroundEnemies()
    {
        // Clear the existing list
        groundEnemies.Clear();

        // Find all objects with the "GroundEnemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("GroundEnemy");

        // Add each enemy to the list
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                groundEnemies.Add(enemy);
            }
        }
    }

    // Check if all ground enemies have been destroyed
    public void EnemyCheck()
    {
        // Remove null references from the list
        groundEnemies.RemoveAll(enemy => enemy == null);

        // Check if there are no enemies left
        if (groundEnemies.Count == 0)
        {
            OpenDoor();
            Destroy(enemyArea);
            enemyAreaTwo.SetActive(true);
            //anim.SetBool("areAllEnemiesDead", false);
        }
    }

    // Open the door
    private void OpenDoor()
    {
        if (door != null && anim != null)
        {
            anim.SetBool("areAllEnemiesDead", true);
        }
        else
        {
            Debug.Log("Door reference or Animator component is not assigned.");
        }
    }
}
