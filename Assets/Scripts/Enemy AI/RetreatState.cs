using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

/*In short the AI after attacking needs to run away from the player, 
so the AI needs to return to surround state and reenter into the queue*/

[System.Serializable]
public class RetreatState : SimpleState
{
    private NavMeshAgent agent;
    [SerializeField]
    private EnemyFinder enemyTatic;
    public float attackRange;
    public float retreatDistance = 8f;
    private Vector3 retreatPosition;
    private bool reachedRetreatPoint = false;

    public override void OnStart()
    {
        base.OnStart();

        reachedRetreatPoint = false;

        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        enemyTatic = agent.GetComponentInParent<EnemyFinder>();
        retreatPosition = CalculateRetreatPosition(stateMachine);
        if(agent.isOnNavMesh == true)
        {
            agent.SetDestination(retreatPosition);
        }

        if(enemyTatic.nearbyEnemies.Count <= 2)
        {
            stateMachine.ChangeState(nameof(ChargeState));
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                if(agent.isOnNavMesh == true)
                {
                    if (!reachedRetreatPoint && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        reachedRetreatPoint = true;
                        stateMachine.ChangeState(nameof(SurroundState));
                    }
                }
            }
        }

        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            if (rangeGruntStateMachine.isAlive && rangeGruntStateMachine.LOS)
            {
                if(agent.isOnNavMesh == true)
                {
                    if (!reachedRetreatPoint && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        reachedRetreatPoint = true;
                        stateMachine.ChangeState(nameof(ChargeState));
                    }
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private Vector3 CalculateRetreatPosition(SimpleStateMachine stateMachine)
    {
        Transform target = null;

        if (stateMachine is GruntStateMachine grunt)
        {
            target = grunt.target;
        }
        else if (stateMachine is RangeGruntStateMachine rangeGrunt)
        {
            target = rangeGrunt.target;
        }
        Vector3 directionAway = (stateMachine.transform.position - target.position).normalized;
        Vector3 proposedPosition = stateMachine.transform.position + directionAway * retreatDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(proposedPosition, out hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return proposedPosition;
    }
}
