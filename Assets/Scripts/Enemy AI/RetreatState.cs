using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

/*Return to surround state and reenter into the queue*/

[System.Serializable]
public class RetreatState : SimpleState
{
    private NavMeshAgent agent;
    private float attackRange;

    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is GruntStateMachine)
        {
            agent = ((GruntStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            attackRange = ((GruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                agent.SetDestination(gruntStateMachine.target.position);
                
                if (Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) < attackRange)
                {
                    stateMachine.ChangeState(nameof(SurroundState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
