using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*Attacking few a seconds the return to the surround state via retreat state*/

[System.Serializable]
public class AttackState : SimpleState
{
    public Timer time;
    public UnityEvent attack;  
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    private Animator anim;
    private float attackRange;
    public float attackTimer;
    public float cooldownTimer = 3f;
    private float attackDuration = 3f;
    public bool isAttacking;

    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            agent.enabled = true;
            anim = gruntStateMachine.GetComponentInChildren<Animator>();
            agent.SetDestination(gruntStateMachine.transform.position);
            attackRange = gruntStateMachine.inAttackRange + 0.5f;
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agent = agroGruntStateMachine.GetComponent<NavMeshAgent>();
            agent.enabled = true;
            anim = agroGruntStateMachine.GetComponentInChildren<Animator>();
            agent.SetDestination(agroGruntStateMachine.transform.position);
            attackRange = agroGruntStateMachine.inAttackRange + 0.5f;
        }

        if (attack == null)
        {
            attack = new UnityEvent();
        }

        attackTimer = attackDuration;
        cooldownTimer = 0f;
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.transform.LookAt(gruntStateMachine.target);
            
            attackTimer -= _dt; 
            cooldownTimer -= _dt;

            if(agent.isOnNavMesh == true)
            {
                if (gruntStateMachine.LOS && attackTimer > 0f)
                {
                    if (cooldownTimer <= 0f && !isAttacking)
                    {
                        attack.Invoke();
                        isAttacking = true;

                        cooldownTimer = attackDuration;
                    }
                }
                else if(Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) > gruntStateMachine.inAttackRange || attackTimer <= 0f)// Retreat when the attack timer runs out or if the player is out of range
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(RetreatState));
                }
            }
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agroGruntStateMachine.transform.LookAt(agroGruntStateMachine.target);
            
            attackTimer -= _dt; 
            cooldownTimer -= _dt;

            if(agent.isOnNavMesh == true)
            {
                if (agroGruntStateMachine.LOS && attackTimer > 0f)
                {
                    if (cooldownTimer <= 0f && !isAttacking)
                    {
                        attack.Invoke();
                        isAttacking = true;

                        cooldownTimer = attackDuration;
                    }
                }
                else if(Vector3.Distance(agent.transform.position, agroGruntStateMachine.target.position) > agroGruntStateMachine.inAttackRange || attackTimer <= 0f)// Run at that bitch of a player again
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(ChargeState));
                }
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
