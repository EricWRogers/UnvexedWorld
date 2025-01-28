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

    public float mag, power;
    public Vector3 dir;
    //public float knockBackDuration = 0.5f;
    //private float knockBackTimer;

    public override void OnStart()
    {
        Debug.Log("Knock Back State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            agent = meleeStateMachine.GetComponent<NavMeshAgent>();
            rb = meleeStateMachine.GetComponent<Rigidbody>();
        }
        
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(-(dir * (power + mag)), ForceMode.Impulse);

        //knockBackTimer = knockBackDuration;  
        agent.enabled = false;

        
    }

    public override void UpdateState(float dt)
    {
        
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if (meleeStateMachine.isIdling == false)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
            else if(meleeStateMachine.isIdling == true)
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
        rb.useGravity = false;

    }
}
