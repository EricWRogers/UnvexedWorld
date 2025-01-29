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
    public float knockBackDuration = 0.5f;
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

        knockBackTimer = knockBackDuration;  
        agent.enabled = false;

        
    }

    public override void UpdateState(float dt)
    {
        
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if(knockBackTimer > 0)
            {
                knockBackTimer -= dt;
            }
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

        agent.enabled = true;
        rb.isKinematic = true;

    }
}
