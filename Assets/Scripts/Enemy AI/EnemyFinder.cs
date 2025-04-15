using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using System;

public class EnemyFinder : MonoBehaviour
{
    public List<SimpleStateMachine> nearbyEnemies = new List<SimpleStateMachine>();
    
    public int totalEnemies; // Track total number of enemies
    public int defeatedEnemies; // Track number of defeated enemies
    [SerializeField]
    private SlotManager slotManager;

    public bool openDoor;

    void Start()
    {
        totalEnemies = nearbyEnemies.Count + 1; // Set the total number of enemies in the zone
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
        nearbyEnemies.RemoveAll(enemy => enemy == null || enemy.gameObject == null);

        if(defeatedEnemies == totalEnemies)
        {
            GameManager.Instance.battleOn = false;
        }

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
            var agroGruntStateMachine = other.GetComponent<AgroGruntStateMachine>();
            if (agroGruntStateMachine != null && !nearbyEnemies.Contains(agroGruntStateMachine))
            {
                nearbyEnemies.Add(agroGruntStateMachine);

                Health enemyHealth = agroGruntStateMachine.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.outOfHealth.AddListener(() => OnEnemyDefeated(agroGruntStateMachine));
                }
            }
            var rangeGruntStateMachine = other.GetComponent<RangeGruntStateMachine>();
            if (rangeGruntStateMachine != null && !nearbyEnemies.Contains(rangeGruntStateMachine))
            {
                nearbyEnemies.Add(rangeGruntStateMachine);

                Health enemyHealth = rangeGruntStateMachine.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.outOfHealth.AddListener(() => OnEnemyDefeated(rangeGruntStateMachine));
                }
            }
        }
        if(other.gameObject.CompareTag("Player"))
        {
            if(slotManager != null)
            {
                slotManager.SetSlotCount(nearbyEnemies.Count);
            }
        }
    }

    private void OnEnemyDefeated(SimpleStateMachine enemy)
    {
        if (nearbyEnemies.Contains(enemy))
        {
            nearbyEnemies.Remove(enemy);
            defeatedEnemies++;
        }
    }

}
