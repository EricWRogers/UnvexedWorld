using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class RangeStateMachine : SimpleStateMachine
{
    public RandomMovementState movement;
    public StunState stunned;
    public InRangeState inRange;
    public WindUpState windUp;
    public AttackState range;
    public CooldownState cooldown;

    public bool canRotate = true;
    public bool LOS;
    public bool isAlive;
    public bool isClose;
    public bool isSearching;
    public bool isPunched;

    public float ranMinFlee;
    public float ranMaxFlee;
    public float inAttackRange = 1.0f;

    public Transform target;
    private Health health;
    [HideInInspector]
    public Knockback enemyKnockback;
    [HideInInspector]
    public Vector3 lastKnownPlayerPosition;

    void Awake()
    {
        states.Add(movement);
        states.Add(stunned);
        states.Add(inRange);
        states.Add(windUp);
        states.Add(range);
        states.Add(cooldown);

        foreach (SimpleState s in states)
        {
            s.stateMachine = this;
        }
    }

    void Start()
    {
        health = gameObject.GetComponent<Health>();

        enemyKnockback = gameObject.GetComponent<Knockback>();
        
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

        LOS = gameObject.GetComponent<LOS>().targetsInSight;

        if (isPunched && stunned.CanEnterStunState())
        {
            Debug.Log("Should Enter Stun State");
            ChangeState(nameof(StunState));
            isPunched = false;
        }
        
        stunned.UpdateCooldown(Time.deltaTime);
    }

    public void TakenDamage()
    {
        isPunched = true;
    }
}