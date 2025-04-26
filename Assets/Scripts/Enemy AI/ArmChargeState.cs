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

        anim.SetBool("isCharging", true);
        anim.SetFloat("SpeedMultipler", 3.5f);

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            attackRange = bossStateMachine.inAttackRange + 0.5f;
            bossStateMachine.transform.LookAt(bossStateMachine.target);
        }

        attackTimer = attackDuration;
        cooldownTimer = 0f;

        ChargeAttack();
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            if (agent.isOnNavMesh)
            {
                if (isCharging)
                {
                    ChargeMove(_dt);
                }
            }
            else
            {
                Debug.LogWarning("Not on NavMesh");
                isCharging = false;
                stateMachine.ChangeState(nameof(ChargeState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        anim.SetBool("isCharging", false);
        anim.SetFloat("SpeedMultipler", 1.0f);
        agent.velocity = Vector3.zero;
        agent.SetDestination(agent.transform.position);
    }

    public void ChargeAttack()
    {
        //agent.enabled = false;
        Debug.Log("The Charge Worked");

        isCharging = true;
        chargeTimeElapsed = 0f;
    }

    public void ChargeMove(float dt)
    {
        if (agent.isOnNavMesh)
        {
            chargeDirection = ((BossStateMachine)stateMachine).transform.forward;
            // Move manually during lunge
            agent.Move(chargeDirection * chargeSpeed * dt);

            chargeTimeElapsed += dt;

            if (chargeTimeElapsed >= chargeDuration)
            {
                isCharging = false;
                chargeTimeElapsed = 0;
                stateMachine.ChangeState(nameof(ChargeState));
            }
        }
        else
        {
            Debug.LogWarning("The AI is not on the NavMesh!");
            isCharging = false;
            stateMachine.ChangeState(nameof(ChargeState));
        }
    }
}
