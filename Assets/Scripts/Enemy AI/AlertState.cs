using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.StateMachine;

[System.Serializable]
public class AlertState : SimpleState
{
    public bool enteredAlert = false;
    private NavMeshAgent agent;
    private ParticleSystem dustPS;
    private float alertTime = 5f; 
    private float alertTimer = 0f; 

    public override void OnStart()
    {
        //Debug.Log("Alert State");
        base.OnStart();

        enteredAlert = true;

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((MeleeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
            AlertNearbyEnemies();
        }
    }

    public override void UpdateState(float dt)
    {
        alertTimer += dt; 
        
        if (((MeleeStateMachine)stateMachine).isAlive && ((MeleeStateMachine)stateMachine).LOS)
        {
            agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
            dustPS.Play();

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if (alertTimer >= alertTime)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        alertTimer = 0f; 
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
                    NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
                    if (enemyAgent != null)
                    {
                        enemyAgent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
                    }

                    enemyStateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }
    }
}
