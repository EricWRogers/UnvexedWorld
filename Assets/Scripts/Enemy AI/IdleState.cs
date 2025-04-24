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
    private Animator anim;
    public override void OnStart()
    {
        //Debug.Log("Idle State");
        base.OnStart();
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {    
            anim = gruntStateMachine.GetComponentInChildren<Animator>();
        }
        
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {    
            anim = agroGruntStateMachine.GetComponentInChildren<Animator>();
        }

        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {    
            anim = rangeGruntStateMachine.GetComponentInChildren<Animator>();
        }

        if (stateMachine is JumperStateMachine jumperStateMachine)
        {
            anim = jumperStateMachine.GetComponentInChildren<Animator>();
        }

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            anim = bossStateMachine.GetComponentInChildren<Animator>();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {    
            if (gruntStateMachine.isAlive == true)
            {   
                if(gruntStateMachine.LOS == true)
                {
                    //anim.SetTrigger("LOS");
                    stateMachine.ChangeState(nameof(InRangeState));
                }
            }
            
        }

        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {    
            if (agroGruntStateMachine.isAlive == true)
            {   
                if(agroGruntStateMachine.LOS == true)
                {
                    //anim.SetTrigger("LOS");
                    stateMachine.ChangeState(nameof(ChargeState));
                }
            }
        }

        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {    
            if (rangeGruntStateMachine.isAlive == true)
            {   
                if(rangeGruntStateMachine.LOS == true)
                {
                    //anim.SetTrigger("LOS");
                    stateMachine.ChangeState(nameof(InRangeState));
                }
            }
        }

        if (stateMachine is JumperStateMachine jumperStateMachine)
        {    
            if (jumperStateMachine.isAlive == true)
            {   
                if(jumperStateMachine.LOS == true)
                {
                    //anim.SetTrigger("LOS");
                    stateMachine.ChangeState(nameof(ChargeState));
                }
            }
        }

        if (stateMachine is BossStateMachine bossStateMachine)
        {    
            if (bossStateMachine.isAlive == true)
            {   
                if(bossStateMachine.LOS == true)
                {
                    //anim.SetTrigger("LOS");
                    stateMachine.ChangeState(nameof(RoarState));
                }
            }
            
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();

    }
}
