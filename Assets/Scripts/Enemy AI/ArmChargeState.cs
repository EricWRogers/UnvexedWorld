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
    public Vector3 chargeDirection;
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

            if (agent.isOnNavMesh == true)
            {
                if (!isAttacking)
                {

                    isAttacking = true;
                    anim.SetBool("isCharging", true);
                    anim.SetFloat("SpeedMultipler", 3.5f);
                    attack.Invoke(); // Trigger attack event
                }

                if (isCharging)
                {
                    ChargeMove(_dt);
                }

                if (chargeDuration <= 0 && !isCharging)
                {
                    ChargeAttack();
                }

                if (Vector3.Distance(agent.transform.position, bossStateMachine.target.position) > bossStateMachine.inAttackRange)
                {
                    if (chargeDuration <= 0)
                    {
                        isAttacking = false;
                        stopAttacking.Invoke();
                        stateMachine.ChangeState(nameof(ChargeState));
                    }
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        anim.SetBool("isCharging", false);
        anim.SetFloat("SpeedMultipler", 1.0f);
    }

    public void ChargeAttack()
    {
        //agent.enabled = false;
        Debug.Log("The Charge Worked");

        isCharging = true; // Mark lunge as in progress
        chargeTimeElapsed = 0f;
        chargeDirection = (((BossStateMachine)stateMachine).target.position - agent.transform.position).normalized;
    }

    public void ChargeMove(float dt)
    {
        if (agent.isOnNavMesh)
        {
            // Move manually during lunge
            agent.Move(chargeDirection * chargeSpeed * dt);

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
