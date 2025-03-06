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
            agent.enabled = true;
            range = ((GruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is AgroGruntStateMachine)
        {
            agent = ((AgroGruntStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.enabled = true;
            range = ((AgroGruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is RangeGruntStateMachine)
        {
            agent = ((RangeGruntStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.enabled = true;
            range = ((RangeGruntStateMachine)stateMachine).inAttackRange + 0.5f;
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
                    gruntStateMachine.transform.LookAt(gruntStateMachine.target);
                    agent.SetDestination(gruntStateMachine.target.position);
                
                    if (Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) < range)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                }
            }
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            if (agroGruntStateMachine.isAlive && agroGruntStateMachine.LOS)
            {
                if(agent.isOnNavMesh == true)
                {
                    agroGruntStateMachine.transform.LookAt(agroGruntStateMachine.target);
                    agent.SetDestination(agroGruntStateMachine.target.position);
                
                    if (Vector3.Distance(agent.transform.position, agroGruntStateMachine.target.position) < range)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                }
            }
        }
        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            if (rangeGruntStateMachine.isAlive && rangeGruntStateMachine.LOS)
            {
                if(agent.isOnNavMesh == true)
                {
                    rangeGruntStateMachine.transform.LookAt(rangeGruntStateMachine.target);
                    agent.SetDestination(rangeGruntStateMachine.target.position);
                
                    if (Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) < range)
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
