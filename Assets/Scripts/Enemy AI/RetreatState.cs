using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

/*In short the AI after attacking needs to run away from the player, 
so the AI needs to return to surround state and reenter into the queue*/

[System.Serializable]
public class RetreatState : SimpleState
{
    private NavMeshAgent agent;
    public float attackRange;

    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is GruntStateMachine)
        {
            agent = ((GruntStateMachine)stateMachine).GetComponent<NavMeshAgent>();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                //Instead of going towards the player we need to do the opposite!!!
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
