using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.StateMachine;


[System.Serializable]
public class RandomMovementState : SimpleState
{
    private NavMeshAgent agent;
    private Transform agentTransform;
    public float range; 

    public override void OnStart()
    {
        Debug.Log("Move State");
        base.OnStart();

        // if (stateMachine is RangedStateMachine)
        // {
        //     agent = ((RangedStateMachine)stateMachine).GetComponent<NavMeshAgent>();
        // }

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agentTransform = ((MeleeStateMachine)stateMachine).transform;
        }

        // if (stateMachine is RangedStateMachine)
        // {
        //     if(((RangedStateMachine)stateMachine).isAlive == true)
        //         agent.SetDestination(((RangedStateMachine)stateMachine).target.position);
        // }

        if (stateMachine is MeleeStateMachine)
        {
            if (((MeleeStateMachine)stateMachine).isAlive == true)
            {
                agent.SetDestination(((MeleeStateMachine)stateMachine).target.position);
            }
        }
        
        
    }
    
    public override void UpdateState(float dt)
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(agentTransform.position, range, out point)) //pass in our centre point and radius of area
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        { 
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    
}
