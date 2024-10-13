using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class InRangeState : SimpleState
{
    private NavMeshAgent agent;
    [SerializeField]
    private ParticleSystem dustPS;
    private float attackRange;

    public override void OnStart()
    {
        //Debug.Log("Move State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((MeleeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
            attackRange = ((MeleeStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is AgroMeleeStateMachine)
        {
            agent = ((AgroMeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((AgroMeleeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
            attackRange = ((AgroMeleeStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is RangeStateMachine)
        {
            agent = ((RangeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((RangeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
            attackRange = ((RangeStateMachine)stateMachine).inAttackRange;
        }
        
        
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is MeleeStateMachine meleeStateMachine)
    {
        if (meleeStateMachine.isAlive && meleeStateMachine.LOS)
        {
            agent.SetDestination(meleeStateMachine.target.position);
            if (!dustPS.isPlaying)
            {
                dustPS.Play();
            }
            if (Vector3.Distance(agent.transform.position, meleeStateMachine.target.position) < attackRange)
            {
                dustPS.Stop();
                stateMachine.ChangeState(nameof(AttackState));
            }
        }
    }
    else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
    {
        if (agroMeleeStateMachine.isAlive)
        {
            agent.SetDestination(agroMeleeStateMachine.target.position);
            if (!dustPS.isPlaying)
            {
                dustPS.Play();
            }
            if (Vector3.Distance(agent.transform.position, agroMeleeStateMachine.target.position) < attackRange)
            {
                dustPS.Stop();
                stateMachine.ChangeState(nameof(AttackState));
            }
        }
    }
    else if (stateMachine is RangeStateMachine rangeStateMachine)
    {
        if (rangeStateMachine.isAlive)
        {
            agent.SetDestination(rangeStateMachine.target.position);
            if (!dustPS.isPlaying)
            {
                dustPS.Play();
            }
            if (Vector3.Distance(agent.transform.position, rangeStateMachine.target.position) < attackRange)
            {
                dustPS.Stop();
                stateMachine.ChangeState(nameof(WindUpState));
            }
        }
    }

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
