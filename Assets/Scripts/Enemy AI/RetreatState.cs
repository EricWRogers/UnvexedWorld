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

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            agent.enabled = true;
            enemyTatic = agent.GetComponentInParent<EnemyFinder>();
            retreatPosition = CalculateRetreatPosition(gruntStateMachine);
            if(agent.isOnNavMesh == true)
            {
                agent.SetDestination(retreatPosition);
            }

            if(enemyTatic.nearbyEnemies.Count <= 2)
            {
                stateMachine.ChangeState(nameof(ChargeState));
            }
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
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private Vector3 CalculateRetreatPosition(GruntStateMachine gruntStateMachine)
    {
        Vector3 directionAway = (gruntStateMachine.transform.position - gruntStateMachine.target.position).normalized;
        return gruntStateMachine.transform.position + directionAway * retreatDistance;
    }
}
