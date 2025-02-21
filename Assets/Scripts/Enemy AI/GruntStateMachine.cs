using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class GruntStateMachine : SimpleStateMachine
{
    public RandomMovementState randomMovement;
    public IdleState idle;
    public StunState stunned;
    public KnockBackState knockBack;
    public InRangeState chase;
    public SurroundState surround;
    public ChargeState charge;
    public AttackState melee;
    public RetreatState retreat;
    public DeathState dead;

    public Transform target;
    
    private Rigidbody rb;
    private Health health;
    
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    public LayerMask mask;

    public bool LOS;
    public bool isAlive;
    public bool isInsideCollider = false;
    public bool canStun;
    public bool isIdling;
    public bool isGrounded = false;
    public float maxSpeed = 75f;
    public float groundCheckDistance;
    public float bufferCheckDistance = 1f;
    public float gravityScale = 1.0f;
    public static float enemyGravity = -9.81f;

    public float inAttackRange = 1.0f;

    void Awake()
    {
        states.Add(randomMovement);
        states.Add(idle);
        states.Add(stunned);
        states.Add(knockBack);
        states.Add(chase);
        states.Add(surround);
        states.Add(charge);
        states.Add(melee);
        states.Add(retreat);
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

        if (canStun && stunned.CanEnterStunState())
        {
            ChangeState(nameof(StunState));
            canStun = false;
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

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        
        Vector3 gravity = enemyGravity * gravityScale * Vector3.up;
        groundCheckDistance = (GetComponent<CapsuleCollider>().height/2) + bufferCheckDistance;
        Debug.DrawRay(transform.position - (Vector3.up * (GetComponent<CapsuleCollider>().height/2)), -transform.up, Color.red, groundCheckDistance);
        RaycastHit hit;
        if(Physics.Raycast(transform.position - (Vector3.up * (GetComponent<CapsuleCollider>().height/2)), -Vector3.up, out hit, groundCheckDistance, mask))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name + hit.point);
            isGrounded = true;
        }else
        {
            isGrounded = false;
        }

        if(!isGrounded)
        {
            rb.isKinematic = false;
            rb.AddForce(gravity, ForceMode.Acceleration);
            if(!agent.isOnNavMesh)
            {
                agent.enabled = false;
            }else
            {
                agent.enabled = true;
            }
        }else
        {
            rb.isKinematic = true;
        }
    }

    void LateUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
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
