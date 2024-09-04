using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class MeleeStateMachine : SimpleStateMachine
{
    public RandomMovementState randomMovement;
    public InRangeState inRange;
    public AttackState melee;
    public FleeState flee;

    public bool LOS;
    public bool isAlive;
    public float ranMinFlee;
    public float ranMaxFlee;
    private Health health;
    public Transform target;

    void Awake()
    {
        states.Add(randomMovement);
        states.Add(inRange);
        states.Add(melee);
        states.Add(flee);

        foreach (SimpleState s in states)
        {
            s.stateMachine = this;
        }
    }

    void Start()
    {
        health = gameObject.GetComponent<Health>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;

        ChangeState(nameof(RandomMovementState));
    }

    void Update()
    {
        if(health.currentHealth < 25)
        {
            ChangeState(nameof(FleeState));
        }
        if(health.currentHealth > 0)
        {
            isAlive = true;
        }else
        {
            isAlive = false;
        }
        LOS = gameObject.GetComponent<LOS>().targetsInSight;
    }
}
