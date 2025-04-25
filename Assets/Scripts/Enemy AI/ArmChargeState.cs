using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*Depending on the on the bool that was set true the attack will be performed here*/

[System.Serializable]
public class ArmChargeState : SimpleState
{
    public UnityEvent attack;  
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    private Animator anim;
    private float attackRange;
    public float attackTimer;
    public float cooldownTimer = 3f;
    private float attackDuration = 3f;
    public float chargeDuration = 1.5f;
    public float chargeTimeElapsed;
    public float chargeSpeed;
    public bool isAttacking;
    public bool isCharging;

    public override void OnStart()
    {
        base.OnStart();

        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        anim = stateMachine.GetComponentInChildren<Animator>();
        agent.SetDestination(stateMachine.transform.position);

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            attackRange = bossStateMachine.inAttackRange + 0.5f;
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

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            bossStateMachine.transform.LookAt(bossStateMachine.target);
            
            attackTimer -= _dt; 
            cooldownTimer -= _dt;

            if(agent.isOnNavMesh == true)
            {
                if (bossStateMachine.LOS && attackTimer > 0f)
                {
                    if (cooldownTimer <= 0f && !isAttacking)
                    {
                        attack.Invoke();
                        isAttacking = true;

                        cooldownTimer = attackDuration;
                    }
                }
                else if(Vector3.Distance(agent.transform.position, bossStateMachine.target.position) > bossStateMachine.inAttackRange || attackTimer <= 0f)// Retreat when the attack timer runs out or if the player is out of range
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

    public void ChargeAttack()
    {
        //agent.enabled = false;
        Debug.Log("The Charge Worked");

        isCharging = true; // Mark lunge as in progress
        chargeTimeElapsed = 0f;
    }

    public void ChargeMove(float dt)
    {
        if (agent.isOnNavMesh)
        {
            Vector3 chargeDirection = ((BossStateMachine)stateMachine).transform.forward;

            // Move manually during lunge
            agent.Move(chargeDirection * chargeSpeed * dt);
            //((WerewolfStateMachine)stateMachine).transform.Translate(lungeDirection * lungeSpeed * dt, Space.World);

            chargeTimeElapsed += dt;

            if (chargeTimeElapsed >= chargeDuration /*|| Vector3.Distance(((BossStateMachine)stateMachine).transform.position, ((BossStateMachine)stateMachine).target.position) < attackRange*/)
            {
                isCharging = false;
                chargeTimeElapsed = 0;
                stopAttacking.Invoke();
                attack.Invoke();
                stateMachine.ChangeState(nameof(ChargeState));
            }
        }
        else
        {
            Debug.LogWarning("The AI is not on the NavMesh!");
            isCharging = false;
            stopAttacking.Invoke();
            stateMachine.ChangeState(nameof(ChargeState));
        }
    }
}
