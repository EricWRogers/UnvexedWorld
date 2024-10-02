using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class AttackState : SimpleState
{
    public Timer time;
    public UnityEvent attack;  
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    private float attackRange;
    private bool playerInRange;
    public bool isAttacking;

    public override void OnStart()
    {
        Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(meleeStateMachine.transform.position);
            attackRange = meleeStateMachine.inAttackRange + 0.5f;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agent = agroMeleeStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(agroMeleeStateMachine.transform.position);
            attackRange = agroMeleeStateMachine.inAttackRange + 0.5f;
        }

        time.StartTimer(2, true);
        if (attack == null)
        {
            attack = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            meleeStateMachine.transform.LookAt(meleeStateMachine.target);
            
            if (meleeStateMachine.LOS && !isAttacking)
            {
                Debug.Log("Attacking");
                isAttacking = true;
                attack.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, meleeStateMachine.target.position) > meleeStateMachine.inAttackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agroMeleeStateMachine.transform.LookAt(agroMeleeStateMachine.target);
            
            if (agroMeleeStateMachine.LOS && !isAttacking)
            {
                Debug.Log("Attacking");
                isAttacking = true;
                attack.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, agroMeleeStateMachine.target.position) > agroMeleeStateMachine.inAttackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
