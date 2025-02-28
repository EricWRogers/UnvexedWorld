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
            surroundRange = Vector3.Distance(agent.transform.position, ((GruntStateMachine)stateMachine).target.position)  + 5.0f;
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
                    
                    if (Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) <= surroundRange)
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
