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
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            anim = gruntStateMachine.anim;
        }
        else if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            anim = meleeStateMachine.anim;
        }

        ParticleManager.Instance.SpawnStunParticles(spawnLocation,spawnLocation.gameObject);

        stunTimer = stunDuration;  
        agent.isStopped = true;
        Debug.Log("Entering Stun State");
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (stunTimer > 0)
            {
                stunTimer -= _dt;
            }
            if (stunTimer <= 0 && gruntStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(stunTimer <= 0 && gruntStateMachine.isIdling == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
            }
        }
        else if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if(stunTimer > 0)
            {
                stunTimer -= _dt;
            }
            if (stunTimer <= 0 && meleeStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(stunTimer <= 0 && meleeStateMachine.isIdling == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        agent.isStopped = false;   // Allow movement again
        cooldownTimer = stunCooldown;  // Reset the cooldown timer
        ParticleManager.Instance.DestroyStunParticles();
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.canStun = false;
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
