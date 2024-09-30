using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

[System.Serializable]
public class StunState : SimpleState
{
    NavMeshAgent agent;
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
            meleeStateMachine.enemyKnockback.knockbackStrength = 1f;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agent = agroMeleeStateMachine.GetComponent<NavMeshAgent>();
            agroMeleeStateMachine.enemyKnockback.knockbackStrength = 1f;
        }
        stunTimer = stunDuration;  
        agent.isStopped = true;    // Stop the AI from moving
        Debug.Log("Entering Stun State");
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);
        if (stunTimer > 0)
        {
            stunTimer -= _dt;
        }
        else
        {
            stateMachine.ChangeState(nameof(InRangeState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        agent.isStopped = false;   // Allow movement again
        cooldownTimer = stunCooldown;  // Reset the cooldown timer
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            meleeStateMachine.enemyKnockback.knockbackStrength = 10.0f;
            meleeStateMachine.isPunched = false;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agroMeleeStateMachine.enemyKnockback.knockbackStrength = 10.0f;
            agroMeleeStateMachine.isPunched = false;
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
