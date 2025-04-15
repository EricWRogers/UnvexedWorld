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
    public List<string> animNames;
    UnityEvent attack;
    UnityEvent stopAttacking;
    NavMeshAgent agent;
    Animator anim;
    public float attackRange;
    public float attackTimer;
    public float cooldownTimer;
    private float attackDuration;
    public bool isAttacking;
    public List<string> attackNames = new List<string>();
    public override void OnStart()
    {
        base.OnStart();

        attackNames = new List<string> { "ArmCharge", "ArmSlam", "ArmSwing", "LegStomp" };

        anim = stateMachine.GetComponentInChildren<Animator>();
        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.SetDestination(stateMachine.transform.position);

        if (anim != null && attackType >= 0 && (int)attackType < attackNames.Count)
        {
            anim.Play(attackNames[(int)attackType]);
        }

        attackTimer = attackDuration;

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

        //Need to figure out which 
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
                    stateMachine.ChangeState(nameof(RetreatState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
