using SuperPupSystems.StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SurroundState : SimpleState
{
    private EnemyFinder enemyFinder;
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField]
    private SlotManager slotManager;
    private int slotIndex = -1;
    private float waitDuration = 4.0f;
    private float waitTimer;
    private Queue<SimpleStateMachine> attackQueue = new Queue<SimpleStateMachine>();
    private bool isReady = false;

    public override void OnStart()
    {
        base.OnStart();
        
        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            target = gruntStateMachine.target;
            agent = gruntStateMachine.agent;
            enemyFinder = agent.GetComponentInParent<EnemyFinder>();
            slotManager = target.GetComponent<SlotManager>();

            if (slotManager == null)
            {
                Debug.LogError("SlotManager is missing on the player!");
                return;
            }

            //slotManager.SetSlotCount(enemyFinder.nearbyEnemies.Count);

            agent.enabled = true;
            gruntStateMachine.transform.LookAt(target);

            // Assign slot for this enemy
            slotIndex = slotManager.Reserve(gruntStateMachine.gameObject);
            if (slotIndex != -1)
            {
                Vector3 slotPosition = slotManager.GetSlotPosition(slotIndex);
                agent.SetDestination(slotPosition);
            }
            else if(slotIndex == -1)
            {
                Debug.LogError($"{gruntStateMachine.name} could not reserve a slot!");
                return;
            }

            // If there are 2 or fewer enemies, skip surrounding logic (Eric's Request)
            if (enemyFinder.nearbyEnemies.Count <= 2)
            {
                stateMachine.ChangeState(nameof(ChargeState));
                return;
            }
        }

        InitializeQueue();
        waitTimer = waitDuration;
    }

    public override void UpdateState(float _dt)
    {
        base.UpdateState(_dt);

        if (stateMachine is GruntStateMachine gruntStateMachine)
        {
            gruntStateMachine.transform.LookAt(target);

            // Reposition enemy to its slot if it has one
            if (slotIndex != -1)
            {
                Vector3 slotPosition = slotManager.GetSlotPosition(slotIndex);
                agent.SetDestination(slotPosition);
            }

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
                slotManager.Release(slotIndex);
                attacker.ChangeState(nameof(ChargeState));

                waitTimer = waitDuration;
                isReady = false;
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        if (slotIndex != -1 && slotManager != null)
        {
            slotManager.Release(slotIndex); // Free up slot
        }
        attackQueue.Clear();
    }

    private void InitializeQueue()
    {
        attackQueue.Clear();
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
}
