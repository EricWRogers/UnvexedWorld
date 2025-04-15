using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*
This verison of the knock back uses the rigidbody method to give them knock back
*/

[System.Serializable]
public class CrystallizedState : SimpleState
{
    NavMeshAgent agent;
    float oldSpeed;
    public float timeFrozen = 1.5f;
    float timer = 0f;
    public override void OnStart()
    {
        base.OnStart();

        agent = stateMachine.GetComponent<NavMeshAgent>();

        if (stateMachine.GetComponent<Health>().currentHealth <= 0)
        {
            stateMachine.ChangeState(nameof(DeathState));
            return;
        }

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.isCrystalized = true;
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agroGruntStateMachine.isCrystalized = true;
        }
        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            rangeGruntStateMachine.isCrystalized = true;
        }

        agent.enabled = false;
        if (stateMachine is BossStateMachine)
        {
            agent.enabled = true;
            oldSpeed = agent.speed;
            agent.speed = oldSpeed / 2.0f;
        }

        timer = timeFrozen;
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);
        if (timeFrozen > 0)
        {
            timer -= dt;
        }
        if (timer <= 0)
        {
            if (stateMachine is GruntStateMachine gruntStateMachine)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
            {
                stateMachine.ChangeState(nameof(ChargeState));
            }
            if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
            {
                stateMachine.ChangeState(nameof(ChargeState));
            }
            if (stateMachine is JumperStateMachine jumperStateMachine)
            {
                stateMachine.ChangeState(nameof(ChargeState));
            }
            if (stateMachine is BossStateMachine bossStateMachine)
            {
                stateMachine.ChangeState(nameof(ChargeState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        agent.enabled = true;
        agent.speed = oldSpeed;
        timer = 0f;
        
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.isCrystalized = false;
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agroGruntStateMachine.isCrystalized = false;
        }
        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            rangeGruntStateMachine.isCrystalized = false;
        }
    }
}
