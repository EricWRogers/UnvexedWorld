using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class BossStateMachine : SimpleStateMachine
{
    public IdleState idle;
    public RoarState roar;
    public StunState stunned;
    public CrystallizedState crystallized;
    public ChargeState charge;
    public ArmChargeState armCharge;
    public ArmSlamState armSlam;
    public LegStompState legStomp;
    public DeathState dead;

    public Transform target;

    private Rigidbody rb;
    [HideInInspector]
    public Health health;
    
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;
    public GameObject ogParent;

    //Phase
    public bool aggroPhase;

    public bool LOS;
    public bool isAlive;
    public bool canStun;
    public float inAttackRange = 1.0f;

    public enum AttackType
    {
        ArmCharge,
        ArmSlam,
        LegStomp
    }

    public AttackType attackType;
    
    void Awake()
    {
        states.Add(idle);
        states.Add(roar);
        states.Add(stunned);
        states.Add(crystallized);
        states.Add(charge);
        states.Add(armCharge);
        states.Add(armSlam);
        states.Add(legStomp);
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

        ChangeState(nameof(IdleState));
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

        if(health.currentHealth <= health.maxHealth*0.5f)
        {
            aggroPhase = true;
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

        //anim.SetFloat("Forward-back", agent.velocity.magnitude * 2);
    }
}
