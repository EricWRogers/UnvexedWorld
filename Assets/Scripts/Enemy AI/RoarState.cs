using UnityEngine;
using SuperPupSystems.StateMachine;

[System.Serializable]
public class RoarState : SimpleState
{
    public float waitDuration = 1.25f;  // Duration of stun
    private float waitTimer;

    public override void OnStart()
    {
        base.OnStart();
        Debug.Log("Do the roar");
        waitTimer = waitDuration;
    }

    public override void UpdateState(float dt)
    {
        base.UpdateState(dt);

        if (waitTimer > 0)
        {
            waitTimer -= dt;
        }

        if (waitTimer <= 0)
        {
            if(stateMachine is BossStateMachine bossStateMachine)
            {
                bossStateMachine.ChangeState(nameof(ChargeState));
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
        ParticleManager.Instance.DestroyBossCharge();
    }
}
