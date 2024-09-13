using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class InRangeState : SimpleState
{
    private NavMeshAgent agent;
    private ParticleSystem dustPS;
    private float attackRange;

    public override void OnStart()
    {
        Debug.Log("Move State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((MeleeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
            attackRange = ((MeleeStateMachine)stateMachine).inAttackRange + 1.0f;
        }
        
        
    }

    public override void UpdateState(float dt)
    {
        if (((MeleeStateMachine)stateMachine).isAlive && ((MeleeStateMachine)stateMachine).LOS)
        {
            agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
            if (!dustPS.isPlaying)
            {
                dustPS.Play();
            }
            if(Vector3.Distance(agent.transform.position, ((MeleeStateMachine)stateMachine).target.position) < attackRange)
            {
                dustPS.Stop();
                stateMachine.ChangeState(nameof(AttackState));
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
