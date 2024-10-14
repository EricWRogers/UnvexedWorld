using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.StateMachine;

[System.Serializable]
public class RandomMovementState : SimpleState
{
    private NavMeshAgent agent;
    private ParticleSystem dustPS;
    private float moveDelay = 2.0f; 
    private float moveTimer = 0f;
    private float minDelay = 1.5f; 
    private float maxDelay = 3.0f; 

    public float range; 
    public Transform circleCenterObject;

    public override void OnStart()
    {
        //Debug.Log("Wander State");
        base.OnStart();

        if (stateMachine is MeleeStateMachine)
        {
            agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((MeleeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
        }
        else if(stateMachine is AgroMeleeStateMachine)
        {
            agent = ((AgroMeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((AgroMeleeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
        }
        else if(stateMachine is RangeStateMachine)
        {
            agent = ((RangeStateMachine)stateMachine).GetComponent<NavMeshAgent>();
            dustPS = ((RangeStateMachine)stateMachine).GetComponentInChildren<ParticleSystem>();
        }

        MoveToRandomPoint();
    }
    
    public override void UpdateState(float dt)
    {
        if (stateMachine is MeleeStateMachine meleeStateMachine)
        {
            if (meleeStateMachine.isAlive == true)
            {
                moveTimer += dt; 

                if (agent.remainingDistance <= agent.stoppingDistance && moveTimer >= moveDelay) 
                {
                    moveTimer = 0f; 
                    moveDelay = Random.Range(minDelay, maxDelay);
                    MoveToRandomPoint();
                }
            }
            
            if (meleeStateMachine.LOS == true)
            {   
                dustPS.Stop();
                stateMachine.ChangeState(nameof(InRangeState));
            }
        }
        if (stateMachine is AgroMeleeStateMachine agroMeleeStateMachine)
        {
            if (agroMeleeStateMachine.isAlive == true)
            {
                moveTimer += dt; 

                if (agent.remainingDistance <= agent.stoppingDistance && moveTimer >= moveDelay) 
                {
                    moveTimer = 0f; 
                    moveDelay = Random.Range(minDelay, maxDelay);
                    MoveToRandomPoint();
                }
            }
            
            if (agroMeleeStateMachine.LOS == true)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
        }
        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            if (rangeStateMachine.isAlive == true)
            {
                moveTimer += dt; 

                if (agent.remainingDistance <= agent.stoppingDistance && moveTimer >= moveDelay) 
                {
                    moveTimer = 0f; 
                    moveDelay = Random.Range(minDelay, maxDelay);
                    MoveToRandomPoint();
                }
            }
            
            if (rangeStateMachine.LOS == true)
            {
                stateMachine.ChangeState(nameof(InRangeState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    // Move to a random point within the specified range of the GameObject
    private void MoveToRandomPoint()
    {
        Vector3 point;
        if (GetRandomPoint(circleCenterObject.position, range, out point))
        {
            agent.SetDestination(point);
            dustPS.Play();
        }
    }

    // Generate a random point within the specified range of the center object
    bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += center;
        randomDirection.y = center.y; // Ensure the point is on the same y level

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