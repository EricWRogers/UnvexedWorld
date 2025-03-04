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
    public enum KnockBackType
    {
        One,
        Two,
        Three
    }

    private Rigidbody rb;
    private NavMeshAgent agent;

    public float mag, power;
    public Vector3 dir;

    public KnockBackType kbType;
    public float knockBackDuration = 1f;
    private float knockBackTimer;

    public override void OnStart()
    {
        Debug.Log("Knock Back State");
        base.OnStart();

        agent = stateMachine.GetComponent<NavMeshAgent>();
        rb = stateMachine.GetComponent<Rigidbody>();
        
        rb.isKinematic = false;
        

        knockBackTimer = knockBackDuration;  
        if(agent.enabled == true)
        {
            agent.enabled = false;
        }
    }

    public override void UpdateState(float dt)
    {
        if (knockBackTimer == knockBackDuration)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            rb = meleeStateMachine.GetComponent<Rigidbody>();
        }
        
        agent.enabled = false;
        rb.isKinematic = false;

        switch(kbType)
        {
            case KnockBackType.One: {
                rb.AddForce(dir * power, ForceMode.Impulse);
                break;
            }
            case KnockBackType.Two: {
                rb.AddForce(dir * (power + mag), ForceMode.Impulse);
                break;
            }
            case KnockBackType.Three: {
                rb.AddForce(-(dir * (power + mag)), ForceMode.Impulse);
                break;
            }
        }
        }

        Debug.Log("The enemy's speed is " + rb.linearVelocity.magnitude);

        knockBackTimer = knockBackDuration;
    }

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isGrounded && rb.linearVelocity.y < 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }
            if(knockBackTimer > 0)
            {
                knockBackTimer -= dt;
            }
            if (knockBackTimer <= 0 && gruntStateMachine.isIdling == false && gruntStateMachine.isGrounded == true)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(knockBackTimer <= 0 && gruntStateMachine.isIdling == true && gruntStateMachine.isGrounded == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
            }
        }
        else if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if (knockBackTimer <= 0 && meleeStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(knockBackTimer <= 0 && meleeStateMachine.isIdling == true)
            {
                stateMachine.ChangeState(nameof(IdleState));
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if(agent.isOnNavMesh == true)
        {
            agent.enabled = true;
        }

        agent.Warp(agent.transform.position);

        rb.isKinematic = true;
    }
}
