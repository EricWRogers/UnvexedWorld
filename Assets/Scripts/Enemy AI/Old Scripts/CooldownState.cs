using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class CooldownState : SimpleState
{
    public Timer timer;
    public float cooldownTime;
    private NavMeshAgent agent;

    public override void OnStart()
    {
        //Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(meleeStateMachine.transform.position);
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agent = agroMeleeStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(agroMeleeStateMachine.transform.position);
        }
        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            agent = rangeStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(rangeStateMachine.transform.position);
        }

        timer.StartTimer(cooldownTime, false);
        timer.timeout.AddListener(CooldownOver);
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if (timer.timeLeft <= 0)
            {
                meleeStateMachine.ChangeState(nameof(InRangeState));
            }
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            if (timer.timeLeft <= 0)
            {
                agroMeleeStateMachine.ChangeState(nameof(InRangeState));
            }
        }
        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            if (timer.timeLeft <= 0)
            {
                rangeStateMachine.ChangeState(nameof(InRangeState));
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }

    public void CooldownOver()
    {
        stateMachine.ChangeState(nameof(InRangeState));
    }
}
