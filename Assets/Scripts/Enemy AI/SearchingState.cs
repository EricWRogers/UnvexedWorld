using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class SearchingState : SimpleState
{
    private NavMeshAgent agent;
    private Vector3 lastPosOfPlayer;
    private float searchDuration = 3.0f; // Time to spend searching
    private float searchTimer = 0f;
    private float lookAroundAngle = 60f; // Angle to rotate for each look
    private float timeBetweenLooks = 1f; // Time to wait before looking again
    private float lookTimer = 0f;

    public override void OnStart()
    {
        //Debug.Log("Move State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();

            ((MeleeStateMachine)stateMachine).LOS = false;

            lastPosOfPlayer = ((MeleeStateMachine)stateMachine).lastKnownPlayerPosition;

            agent.SetDestination(lastPosOfPlayer);
        }
        
        
    }

    public override void UpdateState(float dt)
    {
        if (((MeleeStateMachine)stateMachine).isAlive && !((MeleeStateMachine)stateMachine).LOS)
        {
            searchTimer += dt;
            Debug.Log("Searching State");
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                lookTimer += dt;

                if (lookTimer >= timeBetweenLooks)
                {
                    // Look in a new direction
                    Quaternion targetRotation = Quaternion.Euler(0, agent.transform.eulerAngles.y + lookAroundAngle, 0);
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, dt * 2);
                    lookTimer = 0f;

                    // Check if the player is in sight after rotating
                    if (((MeleeStateMachine)stateMachine).LOS)
                    {
                        ((MeleeStateMachine)stateMachine).isSearching = false;
                        stateMachine.ChangeState(nameof(InRangeState));
                    }
                }
            }

            if (searchTimer >= searchDuration)
            {
                ((MeleeStateMachine)stateMachine).isSearching = false;
                stateMachine.ChangeState(nameof(RandomMovementState));
            }
        } 

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
