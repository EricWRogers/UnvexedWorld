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
    [SerializeField]
    private GameObject prefab;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private CapsuleCollider col;

    public float mag, power;
    public Vector3 dir;

    public KnockBackType kbType;

    public override void OnStart()
    {
        Debug.Log("Knock Back State");
        base.OnStart();

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            rb = gruntStateMachine.GetComponent<Rigidbody>();
            col = gruntStateMachine.GetComponent<CapsuleCollider>();

            if (!gruntStateMachine.isAlive || gruntStateMachine.GetComponent<Health>().currentHealth <= 0)
            {
                stateMachine.ChangeState(nameof(DeathState));
                return; 
            }
        }
        else if(stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            agent = agroGruntStateMachine.GetComponent<NavMeshAgent>();
            rb = agroGruntStateMachine.GetComponent<Rigidbody>();
            col = agroGruntStateMachine.GetComponent<CapsuleCollider>();

            if (!agroGruntStateMachine.isAlive || agroGruntStateMachine.GetComponent<Health>().currentHealth <= 0)
            {
                stateMachine.ChangeState(nameof(DeathState));
                return; 
            }
        }
        else if(stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            agent = rangeGruntStateMachine.GetComponent<NavMeshAgent>();
            rb = rangeGruntStateMachine.GetComponent<Rigidbody>();
            col = rangeGruntStateMachine.GetComponent<CapsuleCollider>();

            if (!rangeGruntStateMachine.isAlive || rangeGruntStateMachine.GetComponent<Health>().currentHealth <= 0)
            {
                stateMachine.ChangeState(nameof(DeathState));
                return; 
            }
        }

        //Spawn Hamester for Knockback
        GameObject obj = GameObject.Instantiate(prefab, agent.transform.position, agent.transform.rotation);
        //Set the obj to be the parent of the agent
        agent.transform.parent = obj.transform;

        switch(kbType)
        {
            case KnockBackType.One: {
                obj.GetComponent<Rigidbody>().AddForce(dir * power, ForceMode.Impulse);
                break;
            }
            case KnockBackType.Two: {
                obj.GetComponent<Rigidbody>().AddForce(dir * (power + mag), ForceMode.Impulse);
                break;
            }
            case KnockBackType.Three: {
            obj.GetComponent<Rigidbody>().AddForce(-(dir * (power + mag)), ForceMode.Impulse);
                break;
            }
        }

        agent.enabled = false;
        col.enabled = false;
        if(stateMachine is GruntStateMachine)
        {
            obj.transform.LookAt(((GruntStateMachine)stateMachine).target);
            ((GruntStateMachine)stateMachine).enabled = false;
        }
        if(stateMachine is AgroGruntStateMachine)
        {
            obj.transform.LookAt(((AgroGruntStateMachine)stateMachine).target);
            ((AgroGruntStateMachine)stateMachine).enabled = false;
        }
        if(stateMachine is RangeGruntStateMachine)
        {
            obj.transform.LookAt(((RangeGruntStateMachine)stateMachine).target);
            ((RangeGruntStateMachine)stateMachine).enabled = false;
        }
        
        rb.Sleep(); 
    }

    public override void UpdateState(float dt)
    {   
        // if (stateMachine is GruntStateMachine gruntStateMachine)
        // {
        //     if (gruntStateMachine.isGrounded && rb.linearVelocity.y < 0)
        //     {
        //         rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        //     }
        //     if(knockBackTimer > 0)
        //     {
        //         knockBackTimer -= dt;
        //     }
        //     if (knockBackTimer <= 0 && gruntStateMachine.isIdling == false && gruntStateMachine.isGrounded == true)
        //     {
        //         stateMachine.ChangeState(nameof(InRangeState));
        //     }
        //     else if(knockBackTimer <= 0 && gruntStateMachine.isIdling == true && gruntStateMachine.isGrounded == true)
        //     {
        //         stateMachine.ChangeState(nameof(IdleState));
        //     }
        // }

        // else if (stateMachine is MeleeStateMachine meleeStateMachine)
        // {
        //     if(knockBackTimer > 0)
        //     {
        //         knockBackTimer -= dt;
        //     }
        //     if (knockBackTimer <= 0 && meleeStateMachine.isIdling == false)
        //     {
        //         stateMachine.ChangeState(nameof(InRangeState));
        //     }
        //     else if(knockBackTimer <= 0 && meleeStateMachine.isIdling == true)
        //     {
        //         stateMachine.ChangeState(nameof(IdleState));
        //     }
        // }
    }
    
    public override void OnExit()
    {
        base.OnExit();

        // rb.linearVelocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;

        // if(agent.isOnNavMesh == true)
        // {
        //     agent.enabled = true;
        // }

        // agent.Warp(agent.transform.position);

        // rb.isKinematic = true;
    }
}
