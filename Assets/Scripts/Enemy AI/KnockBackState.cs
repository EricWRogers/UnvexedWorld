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
public class KnockBackState : SimpleState
{
    private Rigidbody rb;
    private NavMeshAgent agent;
    public float knockBackDuration = 3.0f;
    private float knockBackTimer;

    public override void OnStart()
    {
        Debug.Log("Knock Back State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            rb = meleeStateMachine.GetComponent<Rigidbody>();
        }
        
        rb.linearVelocity = Vector3.zero;
        rb.detectCollisions = true; // not sure if this bool is needed
        rb.useGravity = true;
        rb.isKinematic = false;

        knockBackTimer = knockBackDuration;  
        agent.isStopped = true;
    }

    public override void UpdateState(float dt)
    {
        if (knockBackTimer > 0)
        {
            knockBackTimer -= dt;
        }
        if (knockBackTimer <= 0)
        {
            stateMachine.ChangeState(nameof(InRangeState));
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();

        agent.isStopped = false;
        rb.isKinematic = true;
        rb.useGravity = false;

    }
}
