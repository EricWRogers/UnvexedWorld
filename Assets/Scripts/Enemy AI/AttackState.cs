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
    private Animator anim;
    private float attackRange;
    private bool playerInRange;
    public bool isAttacking;

    public override void OnStart()
    {
        //Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            anim = meleeStateMachine.GetComponentInChildren<Animator>();
            agent.SetDestination(meleeStateMachine.transform.position);
            attackRange = meleeStateMachine.inAttackRange + 0.5f;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agent = agroMeleeStateMachine.GetComponent<NavMeshAgent>();
            anim = agroMeleeStateMachine.GetComponentInChildren<Animator>();
            agent.SetDestination(agroMeleeStateMachine.transform.position);
            attackRange = agroMeleeStateMachine.inAttackRange + 0.5f;
        }
        else if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            rangeStateMachine.canRotate = false;
            agent = rangeStateMachine.GetComponent<NavMeshAgent>();

            anim = rangeStateMachine.anim;
            agent.SetDestination(rangeStateMachine.transform.position);
            attackRange = rangeStateMachine.inAttackRange + 5.0f;
        }

        //time.StartTimer(2, true);
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
                //Debug.Log("Attacking");
                isAttacking = true;
                anim.SetBool("isAttacking", true);
                attack.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, meleeStateMachine.target.position) > meleeStateMachine.inAttackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    anim.SetBool("isAttacking", false);
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
                //Debug.Log("Attacking");
                isAttacking = true;
                anim.SetBool("isAttacking", true);
                attack.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, agroMeleeStateMachine.target.position) > agroMeleeStateMachine.inAttackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    anim.SetBool("isAttacking", false);
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }
        else if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            rangeStateMachine.canRotate = true;
            if (rangeStateMachine.canRotate)
            {
                rangeStateMachine.transform.LookAt(rangeStateMachine.target);
            }

            if (rangeStateMachine.LOS && !isAttacking)
            {
                isAttacking = true;
                anim.SetBool("isRangeAttack", true);
                attack.Invoke();
                time.StartTimer(1.5f, false);
                stateMachine.ChangeState(nameof(InRangeState));
                return;
            }

            if (isAttacking && time.timeLeft <= 0)
            {
                isAttacking = false;
            }

            if (Vector3.Distance(agent.transform.position, rangeStateMachine.target.position) > rangeStateMachine.inAttackRange)
            {
                isAttacking = false;

                anim.SetBool("isRangeAttack", false); 
                stopAttacking.Invoke();
                stateMachine.ChangeState(nameof(InRangeState));
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();

        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            rangeStateMachine.canRotate = true;
        }
    }
}
