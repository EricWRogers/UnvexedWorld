using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class InRangeState : SimpleState
{
    private NavMeshAgent agent;
    private float attackRange = 1.0f;

    public override void OnStart()
    {
        Debug.Log("Move State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
        }
        
        
    }

    public override void UpdateState(float dt)
    {
        if (((MeleeStateMachine)stateMachine).isAlive && ((MeleeStateMachine)stateMachine).LOS)
        {
            agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
            if(Vector3.Distance(agent.transform.position, ((MeleeStateMachine)stateMachine).target.position) < attackRange)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
