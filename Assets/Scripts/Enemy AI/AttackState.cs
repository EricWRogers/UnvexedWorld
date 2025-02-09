using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;
using UnityEngine.Events;

/*Attacking few a seconds the return to the surround state via retreat state*/

[System.Serializable]
public class AttackState : SimpleState
{
    public Timer time;
    public UnityEvent attack;  
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    private Animator anim;
    private float attackRange;
    private bool playerInRange;
    public bool isAttacking;

    public override void OnStart()
    {
        //Debug.Log("Attack State");
        base.OnStart();

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            agent = gruntStateMachine.GetComponent<NavMeshAgent>();
            anim = gruntStateMachine.GetComponentInChildren<Animator>();
            agent.SetDestination(gruntStateMachine.transform.position);
            attackRange = gruntStateMachine.inAttackRange + 0.5f;
        }

        //time.StartTimer(2, true);
        if (attack == null)
        {
            attack = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.transform.LookAt(gruntStateMachine.target);
            
            if (gruntStateMachine.LOS && !isAttacking)
            {
                //Debug.Log("Attacking");
                isAttacking = true;
                anim.SetBool("isAttacking", true);
                attack.Invoke();
            }

            if(Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) > gruntStateMachine.inAttackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    anim.SetBool("isAttacking", false);
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(RetreatState));
                }
            }
        }    }
    
    public override void OnExit()
    {
        base.OnExit();
    }
}
