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
    private float range;

    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is GruntStateMachine)
        {
            agent = ((GruntStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            range = ((GruntStateMachine)stateMachine).inAttackRange + 0.5f;
            agent.SetDestination(((GruntStateMachine)stateMachine).target.position);
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                if(agent.enabled == true)
                {
                    gruntStateMachine.transform.LookAt(gruntStateMachine.target);
                    agent.SetDestination(gruntStateMachine.target.position);
                
                    if (Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) < range)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
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
