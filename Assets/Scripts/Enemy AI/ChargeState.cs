using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*The now chase state*/

[System.Serializable]
public class ChargeState : SimpleState
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
                    stateMachine.ChangeState(nameof(AttackState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
