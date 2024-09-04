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
    private float moveDelay = 2.0f; 
    private float moveTimer = 0f;
    private float minDelay = 1.5f; 
    private float maxDelay = 3.0f; 

    public override void OnStart()
    {
        Debug.Log("Wonder State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            agentTransform = ((MeleeStateMachine)stateMachine).transform;
        }

        MoveToRandomPoint();
        
    }
    
    public override void UpdateState(float dt)
    {
        if (((MeleeStateMachine)stateMachine).isAlive == true)
        { 
            moveTimer += dt; 

            if (agent.remainingDistance <= agent.stoppingDistance && moveTimer >= moveDelay) 
            {
                moveTimer = 0f; 
                moveDelay = Random.Range(minDelay, maxDelay);
                MoveToRandomPoint();
            }
        }

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void MoveToRandomPoint()
    {
        Vector3 point;
        if (GetRandomPoint(agentTransform.position, range, out point))
        {
            agent.SetDestination(point);
            if (((MeleeStateMachine)stateMachine).LOS)
            {
                ((MeleeStateMachine)stateMachine).isSearching = false;
                stateMachine.ChangeState(nameof(InRangeState));
            }
        }
    }

    bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
