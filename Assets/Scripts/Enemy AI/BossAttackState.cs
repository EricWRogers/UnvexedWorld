using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*Depending on the on the bool that was set true the attack will be performed here*/

[System.Serializable]
public class BossAttackState : SimpleState
{
    UnityEvent attack;
    NavMeshAgent agent;
    Animator anim;
    public float attackRange;
    public float attackTimer;
    private float attackDuration;
    public List<string> attackNames = new List<string>();
    public override void OnStart()
    {
        base.OnStart();

        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        anim = stateMachine.GetComponentInChildren<Animator>();
        agent.SetDestination(stateMachine.transform.position);

        if (stateMachine is JumperStateMachine jumperStateMachine)
        {
            attackRange = jumperStateMachine.inAttackRange + 0.5f;
        }

        if (attack == null)
        {
            attack = new UnityEvent();
        }

        attackTimer = attackDuration;
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);

        //Need to figure out which 
        if(stateMachine is BossStateMachine bossStateMachine)
        {
            if(bossStateMachine.armCharge == true)
            {
                bossStateMachine.armCharge = false;
            }
            else if(bossStateMachine.armSlam == true)
            {
                bossStateMachine.armSlam = false;
            }
            else if(bossStateMachine.armSwing == true)
            {
                bossStateMachine.armSwing = false;
            }
            else if(bossStateMachine.legStomp == true)
            {
                bossStateMachine.legStomp = false;
            }
            else if(bossStateMachine.throwPuss == true)
            {
                bossStateMachine.throwPuss = false;
            }
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
