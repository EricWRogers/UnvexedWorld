using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class FogManager : MonoBehaviour
{
    public GameObject group;

    [SerializeField]
    private List<GameObject> groundEnemies = new List<GameObject>();

    void Start()
    {
        groundEnemies.Clear();  // Clear the current list of enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("GroundEnemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.transform.IsChildOf(group.transform) && enemy.active)  // Only enemies in the current area
            {
                groundEnemies.Add(enemy);
            }
        }

        AddHealthListeners();
    }

    public void Update()
    {
        if (groundEnemies.Count == 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Remove a specific enemy from the list when they die
    public void RemoveEnemy(GameObject enemy)
    {
        if (groundEnemies.Contains(enemy))
        {
            groundEnemies.Remove(enemy);
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
}