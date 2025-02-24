using SuperPupSystems.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*In this state the enemies begin to surround the player
a certain radius given some room for variation, but they 
will enter a queue that will send one enemy after another*/

[System.Serializable]
public class SurroundState : SimpleState
{
    
    public EnemyFinder enemyFinder;
    public float minRadius = 4f;
    public float maxRadius = 8f;
    private float waitDuration = 2.0f;
    public float waitTimer;
    private Queue<SimpleStateMachine> attackQueue = new Queue<SimpleStateMachine>();
    private bool isReady = false;
    private Transform target;
    private NavMeshAgent agent;
    //queue list
    
    public override void OnStart()
    {
        Debug.Log("Entering Surround State");
        base.OnStart();
        if(stateMachine is GruntStateMachine gruntStateMachine)
        {
            target = gruntStateMachine.target;
            agent = gruntStateMachine.agent;
            gruntStateMachine.transform.LookAt(gruntStateMachine.target);
        }
        //start adding to the queue 

        InitializeQueue();
        CircleTarget();

        waitTimer = waitDuration;
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            if (attackQueue.Count == 0)
            {
                InitializeQueue(); 
            }

            if (waitTimer > 0)
            {
                waitTimer -= _dt;
            }

            if (waitTimer <= 0 && !isReady && attackQueue.Count > 0)
            {
                isReady = true;
                SimpleStateMachine attacker = attackQueue.Dequeue();
                stateMachine.ChangeState(nameof(ChargeState));

                waitTimer = waitDuration;
                isReady = false;  
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        attackQueue.Clear();
    }

    private void InitializeQueue()
    {
        if (enemyFinder != null)
        {
            foreach (var enemy in enemyFinder.nearbyEnemies)
            {
                if (enemy is SimpleStateMachine ai)
                {
                    attackQueue.Enqueue(ai);
                }
            }
        }
    }

    private void CircleTarget()
    {
        int count = attackQueue.Count;
        int index = 0;

        foreach (var unit in attackQueue)
        {
            if (unit is SimpleStateMachine enemyState)
            {
                Vector3 position = new Vector3(
                    target.position.x + (Random.Range(minRadius, maxRadius) * Mathf.Cos(2 * Mathf.PI * index / count)),
                    target.position.y,
                    target.position.z + (Random.Range(minRadius, maxRadius) * Mathf.Sin(2 * Mathf.PI * index / count))
                );

                enemyState.transform.LookAt(((GruntStateMachine)stateMachine).target);
                
                if (enemyState.TryGetComponent(out NavMeshAgent enemyAgent))
                {
                    if(enemyAgent.enabled == true)
                    {
                        enemyAgent.SetDestination(position);
                    }
                }

                index++;
            }
        }
    }
}
