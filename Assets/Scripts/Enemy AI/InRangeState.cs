using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*In this state we want the enemies get 
ready to surround the player*/

[System.Serializable]
public class InRangeState : SimpleState
{
    private NavMeshAgent agent;
    private float surroundRange = 5.0f;

    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is GruntStateMachine)
        {
            agent = ((GruntStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.enabled = true;
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                if(agent.isOnNavMesh == true)
                {
                    agent.SetDestination(gruntStateMachine.target.position);

                    float distanceToTarget = Vector3.Distance(agent.transform.position, gruntStateMachine.target.position);
                    float buffer = surroundRange + 1.5f;
                    
                    if (distanceToTarget <= buffer)
                    {
                        stateMachine.ChangeState(nameof(SurroundState));
                    }
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
