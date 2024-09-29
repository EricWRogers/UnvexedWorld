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
        agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
        ((MeleeStateMachine)stateMachine).enemyKnockback.knockbackStrength = 1f;
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
            ((MeleeStateMachine)stateMachine).ChangeState(nameof(InRangeState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        agent.isStopped = false;   // Allow movement again
        cooldownTimer = stunCooldown;  // Reset the cooldown timer
        ((MeleeStateMachine)stateMachine).enemyKnockback.knockbackStrength = 10.0f;
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
