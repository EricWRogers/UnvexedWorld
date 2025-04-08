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

        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        anim = stateMachine.GetComponentInChildren<Animator>();
        agent.SetDestination(stateMachine.transform.position);

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            attackRange = gruntStateMachine.inAttackRange + 0.5f;
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            attackRange = agroGruntStateMachine.inAttackRange + 0.5f;
        }
        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            attackRange = rangeGruntStateMachine.inAttackRange + 6.0f; //I want more flexbilty with the range
        }
        if (stateMachine is JumperStateMachine jumperStateMachine)
        {
            attackRange = jumperStateMachine.inAttackRange + 0.5f;
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
        if (stateMachine is JumperStateMachine jumperStateMachine)
        {
            jumperStateMachine.transform.LookAt(jumperStateMachine.target);
            
            attackTimer -= _dt; 
            cooldownTimer -= _dt;

            if(agent.isOnNavMesh == true)
            {
                if (jumperStateMachine.LOS && attackTimer > 0f)
                {
                    if (cooldownTimer <= 0f && !isAttacking)
                    {
                        attack.Invoke();
                        isAttacking = true;

                        cooldownTimer = attackDuration;
                    }
                }
                else if(Vector3.Distance(agent.transform.position, jumperStateMachine.target.position) > jumperStateMachine.inAttackRange || attackTimer <= 0f)// Run at that bitch of a player again
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(ChargeState));
                }
            }
        }
        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            rangeGruntStateMachine.transform.LookAt(rangeGruntStateMachine.target);

            attackTimer -= _dt; 
            cooldownTimer -= _dt;

            if (agent.isOnNavMesh)
            {
                if (rangeGruntStateMachine.LOS && attackTimer > 0f)
                {
                    if (cooldownTimer <= 0f && !isAttacking)
                    {
                        attack.Invoke();
                        isAttacking = true;

                        cooldownTimer = attackDuration;
                    }
                }
                else if(Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) > rangeGruntStateMachine.inAttackRange || attackTimer <= 0f)
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
