using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using UnityEngine.AI;

[System.Serializable]
public class StunState : SimpleState
{
    NavMeshAgent agent;
    public Material stunMaterial;
    private DamageIndicator enemyIndicator;
    private Animator anim;
    public float stunDuration = 2.0f;  // Duration of stun
    private float stunTimer;
    public float stunCooldown = 5.0f;  // Time before AI can be stunned again
    private float cooldownTimer;

    public override void OnStart()
    {
        Debug.Log("Stun State");
        base.OnStart();
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            anim = meleeStateMachine.anim;
            //meleeStateMachine.enemyKnockback.knockbackStrength = 1f;
            enemyIndicator = meleeStateMachine.GetComponent<DamageIndicator>();
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agent = agroMeleeStateMachine.GetComponent<NavMeshAgent>();
            anim = agroMeleeStateMachine.anim;
            //agroMeleeStateMachine.enemyKnockback.knockbackStrength = 1f;
            enemyIndicator = agroMeleeStateMachine.GetComponent<DamageIndicator>();
        }
        else if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            agent = rangeStateMachine.GetComponent<NavMeshAgent>();
            anim = rangeStateMachine.anim;
            //rangeStateMachine.enemyKnockback.knockbackStrength = 1f;
            enemyIndicator = rangeStateMachine.GetComponent<DamageIndicator>();
        }

        stunTimer = stunDuration;  
        agent.isStopped = true;    // Stop the AI from moving
        enemyIndicator.targetRenderer.material = stunMaterial;
        enemyIndicator.enabled = false;
        Debug.Log("Entering Stun State");
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);
        if (stunTimer > 0)
        {
            stunTimer -= _dt;
            anim.SetBool("isHurt", true);
        }
        if (stunTimer <= 0)
        {
            stateMachine.ChangeState(nameof(InRangeState));
            anim.SetBool("isHurt", false);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        agent.isStopped = false;   // Allow movement again
        cooldownTimer = stunCooldown;  // Reset the cooldown timer
        enemyIndicator.targetRenderer.material = enemyIndicator.defaultMaterial;
        enemyIndicator.enabled = true;
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            //meleeStateMachine.enemyKnockback.knockbackStrength = 5.0f;
            meleeStateMachine.isPunched = false;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            //agroMeleeStateMachine.enemyKnockback.knockbackStrength = 5.0f;
            agroMeleeStateMachine.isPunched = false;
        }
        else if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            //rangeStateMachine.enemyKnockback.knockbackStrength = 5.0f;
            rangeStateMachine.isPunched = false;
        }
        Debug.Log("Exiting Stun State");
    }

    public bool CanEnterStunState()
    {
        return cooldownTimer <= 0;  
    }

    public void UpdateCooldown(float _dt)
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= _dt;
        }
    }
}
