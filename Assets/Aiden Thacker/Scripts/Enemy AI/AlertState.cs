using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.StateMachine;

[System.Serializable]
public class AlertState : SimpleState
{
    private NavMeshAgent agent;

    public override void OnStart()
    {
        Debug.Log("Alert State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            AlertNearbyEnemies();
        }
    }

    public override void UpdateState(float dt)
    {
        if (((MeleeStateMachine)stateMachine).isAlive && ((MeleeStateMachine)stateMachine).LOS)
        {
            agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);

            // Check if the AI is close enough to the player
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                stateMachine.ChangeState(nameof(InRangeState)); // Only switch to InRangeState if in range
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public void AlertNearbyEnemies()
    {
        GameObject[] groundEnemies = GameObject.FindGameObjectsWithTag("GroundEnemy");
        GameObject currentEnemy = ((MeleeStateMachine)stateMachine).gameObject;

        foreach (GameObject enemy in groundEnemies)
        {
            if (enemy != currentEnemy) // Skip the current AI
            {
                MeleeStateMachine enemyStateMachine = enemy.GetComponent<MeleeStateMachine>();

                if (enemyStateMachine != null && enemyStateMachine.isAlive)
                {
                    // Set the target position for the alerted enemy to move toward the player
                    NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
                    if (enemyAgent != null)
                    {
                        enemyAgent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
                    }

                    // Change the state of the enemy to InRangeState after setting the destination
                    enemyStateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }
    }
}
