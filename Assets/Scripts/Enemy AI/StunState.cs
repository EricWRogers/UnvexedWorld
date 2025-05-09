using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using UnityEngine.AI;
using Unity.VisualScripting;

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
        base.OnStart();
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            anim = gruntStateMachine.anim;
        }
        else if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agent = agroGruntStateMachine.GetComponent<NavMeshAgent>();
            anim = agroGruntStateMachine.anim;
        }
        else if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            agent = rangeGruntStateMachine.GetComponent<NavMeshAgent>();
            anim = rangeGruntStateMachine.anim;
        }

        ParticleManager.Instance.SpawnStunParticles(spawnLocation,spawnLocation.gameObject);

        stunTimer = stunDuration;
        if(agent.enabled == true)
        {
            agent.isStopped = true;
        }
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
        else if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            if(stunTimer > 0)
            {
                stunTimer -= _dt;
            }
            if (stunTimer <= 0 && agroGruntStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(stunTimer <= 0 && agroGruntStateMachine.isIdling == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
            }
        }
        else if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            if(stunTimer > 0)
            {
                stunTimer -= _dt;
            }
            if (stunTimer <= 0 && rangeGruntStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(stunTimer <= 0 && rangeGruntStateMachine.isIdling == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        if(agent.enabled == true)
        {
            agent.isStopped = false;
        }
        cooldownTimer = stunCooldown;  // Reset the cooldown timer
        ParticleManager.Instance.DestroyStunParticles();
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.canStun = false;
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agroGruntStateMachine.canStun = false;
        }
        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            rangeGruntStateMachine.canStun = false;
        }
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
