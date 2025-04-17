using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RoarState : SimpleState
{
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log("Do the roar");
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        if(stateMachine is BossStateMachine bossStateMachine)
        {
            bossStateMachine.ChangeState(nameof(ChargeState));
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();

    }
}
