using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class InRangeState : SimpleState
{
    private NavMeshAgent agent;

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
        if (((MeleeStateMachine)stateMachine).isAlive)
        {
            agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);

            if (((MeleeStateMachine)stateMachine).LOS)
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
