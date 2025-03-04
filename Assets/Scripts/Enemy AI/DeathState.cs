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
        Debug.Log("Oh Shit He Dead");
        base.OnStart();
        
    }

    public override void UpdateState(float dt)
    {
        
    }
    
    public override void OnExit()
    {
        base.OnExit();

    }
}
