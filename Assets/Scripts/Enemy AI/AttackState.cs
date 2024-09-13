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
    private bool playerInRange;
    public bool isAttacking;
    private float attackCooldown = 2f;  // Cooldown time in seconds
    private float lastAttackTime;

    public override void OnStart()
    {
        Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agent.SetDestination(((MeleeStateMachine)stateMachine).transform.position);
        }

        lastAttackTime = -attackCooldown;  // Ensures the AI can attack immediately when first entering this state
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is MeleeStateMachine)
        {
            ((MeleeStateMachine)stateMachine).transform.LookAt(((MeleeStateMachine)stateMachine).target);
            
            if (((MeleeStateMachine)stateMachine).LOS && Time.time >= lastAttackTime + attackCooldown)
            {
                Debug.Log("Attacking");
                lastAttackTime = Time.time;
                attack.Invoke();
            }
            if(Vector3.Distance(agent.transform.position, ((MeleeStateMachine)stateMachine).target.position) > ((MeleeStateMachine)stateMachine).inAttackRange)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }

            if (!((MeleeStateMachine)stateMachine).LOS /*&& !((MeleeStateMachine)stateMachine).isClose*/)
            {

                stopAttacking.Invoke();
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
