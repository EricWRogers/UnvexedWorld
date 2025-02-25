using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class KillEnemy : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> groundEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        UpdateGroundEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all ground enemies have been destroyed
        EnemyCheck();

        //Destorys the enemy if the P button is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach(GameObject enemy in groundEnemies)
            {
                Destroy(enemy);
            }
        }
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
    }
}
