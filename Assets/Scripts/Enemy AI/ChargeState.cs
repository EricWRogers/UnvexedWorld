using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

/*The now chase state*/

[System.Serializable]
public class ChargeState : SimpleState
{
    private NavMeshAgent agent;
    private Transform target;
    private float range;
    private float maxRange = 12f;
    private float minRange = 4f;
    private float bossDistance;

    public override void OnStart()
    {
        base.OnStart();

        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.enabled = true;

        if (stateMachine is GruntStateMachine)
        {
            ((GruntStateMachine)stateMachine).isCrystalized = false;
            target = ((GruntStateMachine)stateMachine).target;
            range = ((GruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is AgroGruntStateMachine)
        {
            ((AgroGruntStateMachine)stateMachine).isCrystalized = false;
            target = ((AgroGruntStateMachine)stateMachine).target;
            range = ((AgroGruntStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is RangeGruntStateMachine)
        {
            ((RangeGruntStateMachine)stateMachine).isCrystalized = false;
            target = ((RangeGruntStateMachine)stateMachine).target;
        }

        if (stateMachine is JumperStateMachine)
        {
            target = ((JumperStateMachine)stateMachine).target;
            range = ((JumperStateMachine)stateMachine).inAttackRange + 0.5f;
        }

        if (stateMachine is BossStateMachine)
        {
            target = ((BossStateMachine)stateMachine).target;
            range = ((BossStateMachine)stateMachine).inAttackRange + 1.25f;
            bossDistance = Vector3.Distance(((BossStateMachine)stateMachine).transform.position, target.position);
        }

        agent.SetDestination(target.position);
    }

    public override void UpdateState(float dt)
    {
        if (agent.enabled == true && agent.isStopped)
        {
            agent.SetDestination(target.position);
        }
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (gruntStateMachine.isAlive && gruntStateMachine.LOS)
            {
                if (agent.isOnNavMesh == true)
                {
                    gruntStateMachine.transform.LookAt(gruntStateMachine.target);
                    agent.SetDestination(gruntStateMachine.target.position);

                    gruntStateMachine.isCrystalized = false;

                    if (Vector3.Distance(agent.transform.position, gruntStateMachine.target.position) < range)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                }
            }
        }
        if (stateMachine is AgroGruntStateMachine agroGruntStateMachine)
        {
            if (agroGruntStateMachine.isAlive && agroGruntStateMachine.LOS)
            {
                if (agent.isOnNavMesh == true)
                {
                    agroGruntStateMachine.transform.LookAt(agroGruntStateMachine.target);
                    agent.SetDestination(agroGruntStateMachine.target.position);

                    agroGruntStateMachine.isCrystalized = false;

                    if (Vector3.Distance(agent.transform.position, agroGruntStateMachine.target.position) < range)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                }
            }
        }

        if (stateMachine is JumperStateMachine jumperStateMachine)
        {
            if (jumperStateMachine.isAlive && jumperStateMachine.LOS)
            {
                if (agent.isOnNavMesh == true)
                {
                    jumperStateMachine.transform.LookAt(jumperStateMachine.target);
                    agent.SetDestination(jumperStateMachine.target.position);

                    if (Vector3.Distance(agent.transform.position, jumperStateMachine.target.position) < range)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                }
            }
        }

        if (stateMachine is RangeGruntStateMachine rangeGruntStateMachine)
        {
            if (rangeGruntStateMachine.isAlive && rangeGruntStateMachine.LOS)
            {
                if (agent.isOnNavMesh == true)
                {
                    rangeGruntStateMachine.transform.LookAt(rangeGruntStateMachine.target);
                    rangeGruntStateMachine.isCrystalized = false;

                    if (Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) > maxRange)
                    {
                        agent.SetDestination(rangeGruntStateMachine.target.position);
                    }
                    else if (Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) > minRange && Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) < maxRange)
                    {
                        stateMachine.ChangeState(nameof(AttackState));
                    }
                    else if (Vector3.Distance(agent.transform.position, rangeGruntStateMachine.target.position) < minRange)
                    {
                        stateMachine.ChangeState(nameof(RetreatState));
                    }
                }
            }
        }

        if (stateMachine is BossStateMachine bossStateMachine)
        {
            if (bossStateMachine.isAlive && bossStateMachine.LOS && agent.isOnNavMesh)
            {
                bossStateMachine.transform.LookAt(bossStateMachine.target);
                float distanceToTarget = Vector3.Distance(agent.transform.position, bossStateMachine.target.position);

                // Keep chasing if far
                if (distanceToTarget > 10f)
                {
                    agent.SetDestination(bossStateMachine.target.position);
                }

                // Only choose attack if agent is close enough and has arrived
                bool hasArrived = !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;

                if (hasArrived)
                {
                    ChoseMeeleAttack();
                    stateMachine.ChangeState(nameof(BossAttackState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public void ChoseMeeleAttack()
    {
        int a;
        if (stateMachine is BossStateMachine bossStateMachine)
        {
            float rand = Random.value;

            if (rand < 0.75f)
            {
                bossStateMachine.attack.attackType = BossAttackState.AttackType.ArmCharge;
            }
            else
            {
                if (bossStateMachine.aggroPhase)
                {
                    a = Random.Range(0, 3);
                }
                else
                {
                    a = Random.Range(0, 2);
                }

                switch (a)
                {
                    case 0:
                        bossStateMachine.attack.attackType = BossAttackState.AttackType.ArmCharge;
                        break;
                    case 1:
                        bossStateMachine.attack.attackType = BossAttackState.AttackType.ArmSlam;
                        break;
                    case 2:
                        bossStateMachine.attack.attackType = BossAttackState.AttackType.LegStomp;
                        break;
                }
            }
        }
    }
}
