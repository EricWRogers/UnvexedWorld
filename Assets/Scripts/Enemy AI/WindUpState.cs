using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*
* This State is used to wind up the attack for telegraphing the attack
*/
[System.Serializable]
public class WindUpState : SimpleState
{
    public Timer timer;
    public UnityEvent windUp;  
    public UnityEvent stopWindUp;
    public float distanceRange;
    private NavMeshAgent agent;
    private float windUpRange;
    public bool isWindUp;
    public override void OnStart()
    {
        //Debug.Log("windUp State");
        base.OnStart();

        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            agent = rangeStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(rangeStateMachine.transform.position);
            windUpRange = rangeStateMachine.inAttackRange + distanceRange;
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            rangeStateMachine.transform.LookAt(rangeStateMachine.target);

            if (rangeStateMachine.LOS && !isWindUp)
            {
                isWindUp = true;
                windUp.Invoke();
                timer.StartTimer(2f, false); 
            }

            if (isWindUp && timer.timeLeft <= 0)
            {
                stateMachine.ChangeState(nameof(AttackState));
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
