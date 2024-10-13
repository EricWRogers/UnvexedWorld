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

    // Base properties expected for all AI types
    private bool isAlive;
    private bool LOS;

    public override void OnStart()
    {
        base.OnStart();

        // Assign agent and dustPS from the AI game object
        agent = stateMachine.GetComponent<NavMeshAgent>();
        dustPS = stateMachine.GetComponentInChildren<ParticleSystem>();

        // Determine the AI type and assign properties
        if (stateMachine is MeleeStateMachine meleeSM)
        {
            isAlive = meleeSM.isAlive;
            LOS = meleeSM.LOS;
        }
        else if (stateMachine is AgroMeleeStateMachine agroMeleeSM)
        {
            isAlive = agroMeleeSM.isAlive;
            LOS = agroMeleeSM.LOS;
        }
        else if (stateMachine is RangeStateMachine rangeSM)
        {
            isAlive = rangeSM.isAlive;
            LOS = rangeSM.LOS;
        }

        MoveToRandomPoint();
    }

    public override void UpdateState(float dt)
    {
        if (isAlive)
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

    private void MoveToRandomPoint()
    {
        Vector3 point;
        if (GetRandomPoint(circleCenterObject.position, range, out point))
        {
            agent.SetDestination(point);
            dustPS?.Play();

            if (LOS)
            {
                dustPS?.Stop();
                ((MeleeStateMachine)stateMachine).ChangeState(nameof(AlertState));
            }
        }
    }

    bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range + center;
        randomDirection.y = center.y;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
