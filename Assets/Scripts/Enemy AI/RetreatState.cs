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
    public float attackRange;
    public float retreatDistance = 8f;
    private Vector3 retreatPosition;
    private bool reachedRetreatPoint = false;

    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            retreatPosition = CalculateRetreatPosition(gruntStateMachine);
            agent.SetDestination(retreatPosition);
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                if (!reachedRetreatPoint && Vector3.Distance(agent.transform.position, retreatPosition) < 0.5f)
                {
                    reachedRetreatPoint = true;
                    stateMachine.ChangeState(nameof(SurroundState));
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
