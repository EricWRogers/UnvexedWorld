using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class MeleeStateMachine : SimpleStateMachine
{
    public RandomMovementState randomMovement;
    public AlertState alert;
    public InRangeState inRange;
    public AttackState melee;
    
    public bool LOS;
    public bool isAlive;
    public bool isClose;
    public bool isSearching;

    public float ranMinFlee;
    public float ranMaxFlee;
    public float inAttackRange = 1.0f;

    public Transform target;
    private Health health;
    [HideInInspector]
    public Vector3 lastKnownPlayerPosition;

    void Awake()
    {
        states.Add(randomMovement);
        states.Add(alert);
        states.Add(inRange);
        states.Add(melee);

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
        if(health.currentHealth > 0)
        {
            isAlive = true;
        }else
        {
            isAlive = false;
        }

        bool currentLOS = gameObject.GetComponent<LOS>().targetsInSight;
        if (currentLOS)
        {
            // Update last known position if LOS is true
            lastKnownPlayerPosition = target.position;
        }

        LOS = currentLOS;
    }
}