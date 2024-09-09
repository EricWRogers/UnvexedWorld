using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour
{
    private List<GameObject> groundEnemies = new List<GameObject>();
    private Animator anim;

    public GameObject door;
    public GameObject enemyArea;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            anim.SetBool("areAllEnemiesDead", true);
            Destroy(enemyArea);
        }
    }
}
