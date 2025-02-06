using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class MeleeStateMachine : SimpleStateMachine
{
    public RandomMovementState randomMovement;
    public IdleState idle;
    public StunState stunned;
    public KnockBackState knockBack;
    public InRangeState inRange;
    public AttackState melee;

    [HideInInspector]
    public NavMeshAgent agent;
    
    public bool LOS;
    public bool isAlive;
    public bool isInsideCollider = false;
    public bool isSearching;
    public bool isPunched;
    public bool isIdling;

    public float inAttackRange = 1.0f;

    public Transform target;
    private Health health;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Knockback enemyKnockback;
    [HideInInspector]
    public Vector3 lastKnownPlayerPosition;
    private Rigidbody rb;

    void Awake()
    {
        states.Add(randomMovement);
        states.Add(idle);
        states.Add(stunned);
        states.Add(knockBack);
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

        anim = gameObject.GetComponentInChildren<Animator>();

        enemyKnockback = gameObject.GetComponent<Knockback>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        rb = GetComponent<Rigidbody>();

        if(isIdling)
        {
            ChangeState(nameof(IdleState));
        }else
        {
            ChangeState(nameof(RandomMovementState));
        }
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
            ChangeState(nameof(StunState));
            isPunched = false;
        }
        
        stunned.UpdateCooldown(Time.deltaTime);

        if (agent.velocity.magnitude > 0.1f) 
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void TakenDamage()
    {
        isPunched = true;
    }
    
    public void TypeOneKnockBack(Vector3 direction, float power)
    {
        knockBack.dir = direction;
        knockBack.power = power;
        knockBack.kbType = KnockBackState.KnockBackType.One;
        ChangeState(nameof(KnockBackState));
    }

    public void TypeTwoKnockBack(Transform direction, float power)
    {
        float mag = rb.linearVelocity.magnitude;
        Vector3 dir = (transform.position - direction.transform.position).normalized;
        knockBack.mag = mag;
        knockBack.dir = dir;
        knockBack.power = power;
        knockBack.kbType = KnockBackState.KnockBackType.Two;
        ChangeState(nameof(KnockBackState));
    }

    public void TypeThreeKnockBack(Transform direction, float power)
    {
        float mag = rb.linearVelocity.magnitude;
        Vector3 dir = (transform.position - direction.transform.position).normalized;
        knockBack.mag = mag;
        knockBack.dir = dir;
        knockBack.power = power;
        knockBack.kbType = KnockBackState.KnockBackType.Three;
        ChangeState(nameof(KnockBackState));
    }
}
