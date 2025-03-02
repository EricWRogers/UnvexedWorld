using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*
This verison of the Idle does nothing for now execpt after going to and form knockback
*/

[System.Serializable]
public class IdleState : SimpleState
{

    public override void OnStart()
    {
        Debug.Log("Idle State");
        base.OnStart();
        
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {    
            if (gruntStateMachine.isInsideCollider == true && gruntStateMachine.isAlive == true)
            {   
                if(gruntStateMachine.LOS == true)
                {
                    stateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();

    }
}
