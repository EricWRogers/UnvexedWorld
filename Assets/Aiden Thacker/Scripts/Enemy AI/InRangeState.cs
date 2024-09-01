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

        // if (stateMachine is RangedStateMachine)
        // {
        //     agent = ((RangedStateMachine)stateMachine).GetComponent<NavMeshAgent>();
        // }

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
        }

        // if (stateMachine is RangedStateMachine)
        // {
        //     if(((RangedStateMachine)stateMachine).isAlive == true)
        //         agent.SetDestination(((RangedStateMachine)stateMachine).target.position);
        // }
        if (stateMachine is MeleeStateMachine)
        {
            if (((MeleeStateMachine)stateMachine).isAlive == true)
            {
                agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
            }
        }
        
        
    }

    public override void UpdateState(float dt)
    {
        // if (stateMachine is RangedEnemyStateMachine)
        // {
        //     if (((RangedStateMachine)stateMachine).isAlive == true)
        //     {
        //         agent.SetDestination(((RangedEnemyStateMachine)stateMachine).target.position);
        //         if (((RangedEnemyStateMachine)stateMachine).LOS)
        //         {
        //             stateMachine.ChangeState(nameof(AttackState));
        //         }
        //     }


        //     if (((RangedStateMachine)stateMachine).isAlive == true)
        //     {
        //         if (((RangedStateMachine)stateMachine).Flee)
        //         {
        //             stateMachine.ChangeState(nameof(FleeState));
        //         }
        //     }
        // }
        

        if (stateMachine is MeleeStateMachine)
        {
            if (((MeleeStateMachine)stateMachine).isAlive == true)
            { 
                agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
                if (((MeleeStateMachine)stateMachine).LOS)
                {
                    //stateMachine.ChangeState(nameof(AttackState));
                }
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
