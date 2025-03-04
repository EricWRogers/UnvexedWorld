using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using System;

public class EnemyFinder : MonoBehaviour
{
    public List<SimpleStateMachine> nearbyEnemies = new List<SimpleStateMachine>();
    private int totalEnemies; // Track total number of enemies
    private int defeatedEnemies; // Track number of defeated enemies

    private SlotManager slotManager;

    public bool openDoor;

    void Start()
    {
        totalEnemies = nearbyEnemies.Count; // Set the total number of enemies in the zone
        defeatedEnemies = 0; // Initialize defeated enemies count

        slotManager = FindFirstObjectByType<SlotManager>();

        foreach (var enemy in nearbyEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                // Add listener to detect when enemy's health reaches zero
                enemyHealth.outOfHealth.AddListener(() => OnEnemyDefeated(enemy));
            }
        }
    }

    void Update()
    {
        if(nearbyEnemies.Count == 0)
        {
            openDoor = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GroundEnemy") || other.gameObject.CompareTag("Enemy"))
        {
            var gruntStateMachine = other.GetComponent<GruntStateMachine>();
            if (gruntStateMachine != null && !nearbyEnemies.Contains(gruntStateMachine))
            {
                nearbyEnemies.Add(gruntStateMachine);

                Health enemyHealth = gruntStateMachine.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.outOfHealth.AddListener(() => OnEnemyDefeated(gruntStateMachine));
                }
            }
        }
        if(other.gameObject.CompareTag("Player"))
        {
            if(slotManager != null)
            {
                slotManager.count = nearbyEnemies.Count;
            }
        }
    }

    private void OnEnemyDefeated(SimpleStateMachine enemy)
    {
        if (nearbyEnemies.Contains(enemy))
        {
            nearbyEnemies.Remove(enemy);
            defeatedEnemies++;

            Debug.Log($"Enemy defeated! {defeatedEnemies}/{totalEnemies} eliminated.");
        }
    }

}
