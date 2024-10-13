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
    public UnityEvent finishWindUp;
    public float distanceRnage;
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
            windUpRange = rangeStateMachine.inAttackRange + distanceRnage;
        }

        timer.StartTimer(2, true);
        if (windUp == null)
        {
            windUp = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is RangeStateMachine rangeStateMachine)
        {
            rangeStateMachine.transform.LookAt(rangeStateMachine.target);
            
            if (rangeStateMachine.LOS && !isWindUp)
            {
                //Debug.Log("windUping");
                isWindUp = true;
                windUp.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, rangeStateMachine.target.position) > rangeStateMachine.inAttackRange)
            {
                timer.autoRestart = false;
                if (timer.timeLeft <= 0)
                {
                    isWindUp = false;
                    finishWindUp.Invoke();
                    stateMachine.ChangeState(nameof(AttackState));
                }
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
