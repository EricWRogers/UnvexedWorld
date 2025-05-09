using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class JumperStateMachine : SimpleStateMachine
{
    public RandomMovementState randomMovement;
    public IdleState idle;
    public KnockBackState knockBack;
    public ChargeState charge;
    public AttackState melee;
    public DeathState dead;

    public Transform target;
    
    private Rigidbody rb;
    private Health health;
    
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    public GameObject ogParent;

    public bool LOS;
    public bool isAlive;
    public bool canStun;
    public bool isIdling;
    public float inAttackRange = 1.0f;

    void Awake()
    {
        states.Add(randomMovement);
        states.Add(idle);
        states.Add(knockBack);
        states.Add(charge);
        states.Add(melee);
        states.Add(dead);

        foreach (SimpleState s in states)
        {
            s.stateMachine = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = gameObject.GetComponent<Health>();

        anim = gameObject.GetComponentInChildren<Animator>();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        rb = GetComponent<Rigidbody>();

        ogParent  = transform.parent != null ? transform.parent.gameObject : null;

        if(isIdling)
        {
            ChangeState(nameof(IdleState));
        }else
        {
            ChangeState(nameof(RandomMovementState));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth > 1)
        {
            isAlive = true;
        }else
        {
            isAlive = false;
            ChangeState(nameof(DeathState));
        }

        LOS = gameObject.GetComponent<LOS>().targetsInSight;

        if (agent.velocity.magnitude > 0.1f) 
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //Debug.Log("Velocity: " + agent.velocity.magnitude * 2);
        anim.SetFloat("Forward-back", agent.velocity.magnitude * 2);
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
