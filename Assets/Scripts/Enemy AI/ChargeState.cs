using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

/*The now chase state*/

[System.Serializable]
public class ChargeState : SimpleState
{
    private NavMeshAgent agent;
    private Transform target;
    private float range;
    private float maxRange = 12f;
    private float minRange = 4f;

    public override void OnStart()
    {
        base.OnStart();

        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;

        if (stateMachine is GruntStateMachine)
        {
            target = ((GruntStateMachine)stateMachine).target; 
            range = ((GruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is AgroGruntStateMachine)
        {
            target = ((AgroGruntStateMachine)stateMachine).target;
            range = ((AgroGruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is RangeGruntStateMachine)
        {
            target = ((RangeGruntStateMachine)stateMachine).target;
        }
    }

    public override void UpdateState(float dt)
    {
        if(agent.enabled == true && agent.isStopped)
        {
            agent.SetDestination(target.position);
        }
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
                
                    if(Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) > maxRange)
                    {
                        agent.SetDestination(rangeGruntStateMachine.target.position);
                    }
                    else if (Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) > minRange && Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) < maxRange)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                    else if(Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) < minRange)
                    {
                        stateMachine.ChangeState(nameof(RetreatState));
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
