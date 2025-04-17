using UnityEngine;
using SuperPupSystems.StateMachine;

[System.Serializable]
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
