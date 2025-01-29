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
    public Transform spawnLocation;
    public float stunPSLocation = 2f;
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
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agent = agroMeleeStateMachine.GetComponent<NavMeshAgent>();
            anim = agroMeleeStateMachine.anim;
        }
        else if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            agent = rangeStateMachine.GetComponent<NavMeshAgent>();
            anim = rangeStateMachine.anim;
        }

        ParticleManager.Instance.SpawnStunParticles(spawnLocation,spawnLocation.gameObject);

        stunTimer = stunDuration;  
        agent.isStopped = true;
        Debug.Log("Entering Stun State");
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if (stunTimer > 0)
            {
                stunTimer -= _dt;
                anim.SetBool("isHurt", true);
            }
            if (stunTimer <= 0 && meleeStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
                anim.SetBool("isHurt", false);
            }
            else if(stunTimer <= 0 && meleeStateMachine.isIdling == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
                anim.SetBool("isHurt", false);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        agent.isStopped = false;   // Allow movement again
        cooldownTimer = stunCooldown;  // Reset the cooldown timer
        ParticleManager.Instance.DestroyStunParticles();
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            meleeStateMachine.isPunched = false;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            agroMeleeStateMachine.isPunched = false;
        }
        else if (stateMachine is RangeStateMachine rangeStateMachine)
        {
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
