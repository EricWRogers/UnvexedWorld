using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeRangedAttack : MonoBehaviour
{
    public SpellCraft spellCraft;
    PlayerGamepad gamepad;
    public GameObject target;
    public Animator[] animators;

    public float attackRange;

    public ThirdPersonMovement speed;

    public float resetSpeed = 15.0f;

    public float lockUP = 3.0f;

    public bool isAttacking;

    public Animator animator;

    public CameraManager cameraManager;

    public bool direction = false;

    public bool shoot = false;

    public bool punched = false;

    public GameObject lockOnCanvas;

    public CameraLockon cameraLockon;

    public static bool unLock = true;

    public Transform firePoint;

    void Awake()
    {
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.MeleeLight.performed += ctx => MeleeGamepadlight();
        //gamepad.GamePlay.MeleeHeavy.performed += ctx => MeleeGamepadHeavy();
        gamepad.GamePlay.Shoot.performed += ctx => Range();
        gamepad.GamePlay.LockOnTest.performed += ctx => CheckLock();
        gamepad.GamePlay.LockOn.canceled += ctx => LockOff();

         cameraManager = GetComponent<CameraManager>();

         lockOnCanvas.SetActive(false);


    }

   
    void MeleeGamepadlight()
    {
        isAttacking = true;
        punched = true;

        

        if (target == null)
            target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();

        if (target)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < attackRange)
            {

                Vector3 dir = (target.transform.position - transform.position).normalized;
                transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);

                Debug.Log("Found" + target.name);
                
                MeleeLight();

            }
            else
            {
                isAttacking = true;
                MeleeLight();
            }
            if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
            {
                

                FindNewTarget();
            }
        }



    }

    void LockOnTrue()
    {
        unLock = false;
        
    }


    void LockOn()
    {
        direction = true;
        cameraLockon.oneTime = true;
        FindNewTarget();
        if(target != null)
        {
            lockOnCanvas.transform.position = target.transform.position;
            lockOnCanvas.SetActive(true);
            
        }
        unLock = true;
       
       
    }
    
    public void LockOff()
    {
        direction = false;
        lockOnCanvas.SetActive(false);
        unLock = false;
    }
    public void CheckLock()
    {
        if (unLock == false)
        {
            LockOn();
        }
        else
        {
            LockOff();
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
        cameraLockon = GameObject.FindFirstObjectByType<CameraLockon>();

    }
    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }

    
    void Update()
    {
        animator.SetBool("Lock", isAttacking);
        if (isAttacking == true)
        {
            cameraManager.SwitchCamera(cameraManager.meleeCamera);
            speed.baseSpeed = lockUP;
            speed.turnSmoothTime = 10.0f;
            
        }
        else
        {
            cameraManager.SwitchCamera(cameraManager.mainCam);
            speed.baseSpeed = resetSpeed;
            speed.turnSmoothTime = 0.1f;
        }


        if (Input.GetMouseButtonDown(0))
        {
            MeleeGamepadlight();
            

        }
        else if (Input.GetMouseButtonDown(1))
        {
           
            Range();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (unLock == false)
            {
                LockOn();
            }
            else
            {
                LockOff();
            }
        
        }
       
        GetComponent<Animator>().SetBool("CheckDirection", direction);
        GetComponent<Animator>().SetFloat("Directional",Input.GetAxisRaw("Vertical"));

        if(direction == true && target != null)
        {
            lockOnCanvas.transform.position = target.transform.position;
            lockOnCanvas.SetActive(true);
        }

     



    }

    private void MeleeLight()
    {
        GetComponent<Animator>().SetTrigger("Light");
        GetComponentsInChildren<Animator>()[1].SetTrigger("Punch");
    }

    // private void MeleeHeavy()
    // {
    //     GetComponent<Animator>().SetTrigger("Heavy");
    //     GetComponentsInChildren<Animator>()[1].SetTrigger("Punch");
    // }

    private void Range()
    {
        GetComponent<Animator>().SetTrigger("Ranged");
        shoot = true;
       
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

   public void FindNewTarget()
    {

        target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();
    }

    public void FingerGun()
    {
        animator.Play("FingerGun");
    }

    public void LockUp()
    {
        isAttacking=true;
    }

    public void MakeHitBox(int index)
    {
        GameObject temp = Instantiate(AttackManager.Instance.attackPrefabs[index],transform.position + (1f * gameObject.transform.forward), transform.rotation);
        if(temp.GetComponent<AttackUpdater>() != null)
        {
            temp.GetComponent<AttackUpdater>().element = spellCraft.CurrentElement;
            temp.GetComponent<AttackUpdater>().aspect = spellCraft.subAspect;
            temp.GetComponent<AttackUpdater>().player = gameObject;
        }
    }

    public void RangedAttack(int index)
    {
        GameObject temp = Instantiate(AttackManager.Instance.rangeAttackPrefabs[index],firePoint.position + (1f * gameObject.transform.forward), transform.rotation);
        if(temp.GetComponent<AttackUpdater>() != null)
        {
            temp.GetComponent<AttackUpdater>().element = spellCraft.CurrentElement;
            temp.GetComponent<AttackUpdater>().aspect = spellCraft.subAspect;
            temp.GetComponent<AttackUpdater>().player = gameObject;
        }
    }
   
}