using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*Depending on the on the bool that was set true the attack will be performed here*/

[System.Serializable]
public class BossAttackState : SimpleState
{
    public enum AttackType
    {
        ArmCharge,
        ArmSlam,
        ArmSwing,
        LegStomp
    }

    public AttackType attackType;
    public List<string> attackNames = new List<string> { "ArmCharge", "ArmSlam", "ArmSwing", "LegStomp" };
    public UnityEvent attack;
    public UnityEvent stopAttacking;
    NavMeshAgent agent;
    Animator anim;
    public float attackRange;
    public float attackTimer;
    public float cooldownTimer;
    private float attackDuration;
    public bool isAttacking;
    public bool agroPhase;
    private bool isCharging;
    private bool hasAttacked;
    public float chargeDuration = 1.5f;
    public float chargeTimeElapsed;
    public float chargeSpeed;
    public override void OnStart()
    {
        base.OnStart();

        anim = stateMachine.GetComponentInChildren<Animator>();
        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.SetDestination(stateMachine.transform.position);

        attackDuration = GetAttackDuration(attackType);
        attackTimer = attackDuration;
        cooldownTimer = 0f; // Force attack immediately

        if (attack == null)
            attack = new UnityEvent();

        attack.AddListener(WhichAttackAnim);

        // Force attack for testing
        Debug.Log("Forcing boss attack on start!");
        attack.Invoke();
        isAttacking = true;
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            if (bossStateMachine.aggroPhase)
            {
                agroPhase = true;
            }

            bossStateMachine.transform.LookAt(bossStateMachine.target);

            float currentCooldown = agroPhase ? attackDuration * 0.5f : attackDuration;

            attackTimer -= _dt;
            cooldownTimer -= _dt;

            if (agent.isOnNavMesh)
            {
                if (isCharging)
                {
                    ChargeMove(_dt);
                    return;
                }

                bool inRange = Vector3.Distance(agent.transform.position, bossStateMachine.target.position) <= bossStateMachine.inAttackRange;

                if (bossStateMachine.LOS && inRange)
                {
                    if (!hasAttacked && cooldownTimer <= 0f)
                    {
                        attack.Invoke();
                        isAttacking = true;
                        hasAttacked = true;
                        cooldownTimer = currentCooldown;
                    }

                    // // Reset attack after the animation/attack duration ends
                    // if (isAttacking && cooldownTimer <= currentCooldown - 1f)
                    // {
                    //     isAttacking = false;
                    // }
                }
                else
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(ChargeState));
                }

                if (attackTimer <= 0f)
                {
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(ChargeState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        isAttacking = false;
        hasAttacked = false;
        stopAttacking.Invoke();
    }
    private float GetAttackDuration(AttackType type)
    {
        switch (type)
        {
            case AttackType.ArmCharge: return 3f;
            case AttackType.ArmSlam: return 2.5f;
            case AttackType.ArmSwing: return 2f;
            case AttackType.LegStomp: return 3f;
            default: return 2f;
        }
    }

    public void WhichAttackAnim()
    {
        if (anim != null && attackType >= 0 && (int)attackType < attackNames.Count)
        {
            anim.Play(attackNames[(int)attackType]);
        }
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

            if (chargeTimeElapsed >= chargeDuration || Vector3.Distance(((BossStateMachine)stateMachine).transform.position, ((BossStateMachine)stateMachine).target.position) < attackRange)
            {
                isCharging = false;
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
