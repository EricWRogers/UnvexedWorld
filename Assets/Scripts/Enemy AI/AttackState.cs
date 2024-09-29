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

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.SetDestination(((MeleeStateMachine)stateMachine).transform.position);
            attackRange = ((MeleeStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        time.StartTimer(2, true);
        if (attack == null)
        {
            attack = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is MeleeStateMachine)
        {
            ((MeleeStateMachine)stateMachine).transform.LookAt(((MeleeStateMachine)stateMachine).target);
            
            if (((MeleeStateMachine)stateMachine).LOS && !isAttacking)
            {
                Debug.Log("Attacking");
                isAttacking = true;
                attack.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, ((MeleeStateMachine)stateMachine).target.position) > ((MeleeStateMachine)stateMachine).inAttackRange)
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
