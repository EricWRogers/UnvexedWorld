using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeRangedAttack : MonoBehaviour
{
    public SpellCraft spellCraft;
    public SpellShot spellShot;
    PlayerGamepad gamepad;
    private GameObject target;

    public float attackRange;

    public ThirdPersonMovement speed;

    public float resetSpeed = 15.0f;

    private float lockUP = 0.0f;

    public bool isAttacking;

    public Animator animator;

    void Awake()
    {
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Melee.performed += ctx => MeleeGamepad();
        gamepad.GamePlay.Shoot.performed += ctx => Range();

    }

    // Start is called before the first frame update
    void MeleeGamepad()
    {
        isAttacking = true;

        if (target == null)
            target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();

        if (target)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < attackRange)
            {



                Debug.Log("Found" + target.name);
                gameObject.transform.LookAt(target.transform);
                if (spellCraft.casting)
                {
                    spellCraft.CastSpell(SpellCraft.CastType.melee);
                }
                Melee();

            }
            else
            {
                isAttacking = true;
                if (spellCraft.casting)
                {
                    spellCraft.CastSpell(SpellCraft.CastType.melee);
                }
                Melee();
            }
            if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
            {

                FindNewTarget();
            }
        }



    }

    void CancelLockUp()
    {
        isAttacking = false;
    }
    void Start()
    {
        target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();
        animator = GetComponentsInChildren<Animator>()[1];

    }
    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Lock", isAttacking);
        if (isAttacking == true)
        {
            speed.baseSpeed = lockUP;
            speed.turnSmoothTime = 10.0f;
        }
        else
        {
            speed.baseSpeed = resetSpeed;
            speed.turnSmoothTime = 0.1f;
        }


        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
        {
            MeleeGamepad();

        }
        else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E))
        {
            // if (spellCraft.casting && spellCraft.mainAspect!=SpellCraft.Aspect.none)
            // {
            //     spellCraft.CastSpell(SpellCraft.CastType.ranged);
            // }

            Range();
        }


    }

    private void Melee()
    {

        GetComponent<Animator>().SetTrigger("Melee");
        //GetComponent<AnimationForce>().melee = true;
    }

    private void Range()
    {
        GetComponent<Animator>().SetTrigger("Ranged");
        //GetComponent<AnimationForce>().ranged = true;
        //spellShot.ShootPrefab();
    }

    private void UpdateSpells()
    {
        spellCraft.CastSpell(SpellCraft.CastType.melee);
    }

    public void StartParticle()
    {
        GetComponentInChildren<PunchScript>().StartParticle();
    }

    public void EndParticle()
    {
        GetComponentInChildren<PunchScript>().EndParticle();
    }

    public void StartParticleSpell()
    {
        GetComponentInChildren<Spell>().gameObject.GetComponent<PunchScript>().StartParticle();
    }

    public void EndParticleSpell()
    {
        GetComponentInChildren<Spell>().gameObject.GetComponent<PunchScript>().EndParticle();
    }

    void FindNewTarget()
    {

        target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();
    }
}