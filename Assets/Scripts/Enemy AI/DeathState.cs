using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class DeathState : SimpleState
{
    public override void OnStart()
    {
        base.OnStart();
        if(stateMachine is BossStateMachine bossStateMachine)
        {
            bossStateMachine.GetComponentInChildren<Animator>().SetBool("isDead", true);
        }
    }

    public override void UpdateState(float dt)
    {
        
    }
    
    public override void OnExit()
    {
        base.OnExit();

    }
}
