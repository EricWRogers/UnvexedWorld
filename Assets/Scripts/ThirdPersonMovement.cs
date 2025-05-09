using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using SuperPupSystems.Helper;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    PlayerGamepad gamepad;

    Vector2 gamepadMove;

    public CameraManager cameraManager;

    public MeleeRangedAttack lockOn;
    public float gravity = -3.5f;

    public float gravityFirstJump = -5.0f;

    public float gravitySecondJump = -15.0f;

    public float baseSpeed = 6f;

    public float speed = 6f;

    public float airSpeed = 3f;

    public float dashSpeed = 10f;

    public float dashTime = 0.75f;

    public float dashCoolDown = 3.0f;
   
    public float currectDashCoolDown = 0.0f;

    public float turnSmoothTime = 0.1f;

    public bool dashing = false;

    public bool isGrounded = false;

    public bool isJumping;

    public int jumpCount = 0;

    public int jumpMax = 2;

    public float jumpForce = 5f;

    float turnSmoothVelocity;

    Vector3 velocity;

    public Transform cam;

    public Vector3 moveDir;

    private float dashStartTime;

    public bool isSliding;

    private Vector3 slopSlideSpeed;

    public float slopeSpeed = 10.0f;

    public bool rayGround;

    public float groundedCheckDistence;

    private float bufferCheckDistance = 0.1f;

    public Animator animator;

    public GameObject dashLines;

    public LayerMask mask;

     public GameObject groundCheck;

    private RaycastHit m_info;
    private AudioManager audioManager;

    public bool inText = false;

    public bool nextLine = false;

    public bool hasGamePad = false;

    private Vector3 lastPosition;

    private Health health;

   


    void Awake()
    {
        lastPosition = transform.position;
        dashLines = GameObject.Find("DashLines");
        dashLines.SetActive(false);

        gamepad = new PlayerGamepad();

        gamepad.GamePlay.Jump.performed += ctx => GamepadJump();

        gamepad.GamePlay.Dash.performed += ctx => GamepadDash();

        gamepad.GamePlay.Jump.canceled += ctx => NextLine();

        gamepad.GamePlay.Movement.performed += ctx => gamepadMove = ctx.ReadValue<Vector2>();
        gamepad.GamePlay.Movement.canceled += ctx => gamepadMove = Vector2.zero;
    }
    
    void Start()
    {
        animator = GetComponentsInChildren<Animator>()[1];
        Cursor.lockState = CursorLockMode.Locked;   
        lockOn = GetComponent<MeleeRangedAttack>();
        audioManager = FindFirstObjectByType<AudioManager>();
        health = GetComponent<Health>();
        health.hurt.AddListener(AudioManager.instance.PlayPlayerHurtSound);
        
    }

    

    void GamepadJump()
    {
        if(inText == false)
        {
        
            if ((isGrounded || jumpCount < jumpMax  ))
                {
                isJumping = true;
                jumpCount++;
                isGrounded = false;
                velocity.y = 0;
                velocity.y += jumpForce;
                controller.Move(velocity * Time.deltaTime);

                }
        }


      
    }

    void NextLine()
    {
        nextLine = true;
    }
   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TextBox")&& GameManager.Instance.doNothing == true)
        { 
           inText = true;
             
        }
        if(other.gameObject.CompareTag("TextBox") && GameManager.Instance.doNothing == false)
        {
            inText = false;
            
        }
    }

     void OnTriggerExit(Collider other) 
    {
       inText = false;
    }
    

    void Dashing()
    {
        currectDashCoolDown -= Time.deltaTime;
        if (dashing)
        {
            if (Time.time < dashStartTime + dashTime)
            {
                velocity.y -= gravity * Time.deltaTime;
                dashLines.SetActive(true);
                controller.Move(transform.forward * dashSpeed * Time.deltaTime);               
            }
            else
            {
                dashLines.SetActive(false);
                dashing = false;
                currectDashCoolDown = dashCoolDown;
                cameraManager.SwitchCamera(cameraManager.mainCam);
            }
        }

    }

    void GamepadDash()
    {
        if(GameManager.Instance.doNothing == false){
        if ( (!dashing) && currectDashCoolDown <= 0.0f)
        {
            velocity.y -= gravity * Time.deltaTime;

            dashing = true;
            animator.Play("Dash");
            
            dashStartTime = Time.time;
            dashLines.SetActive(true);
            audioManager.PlayDashSound();
            cameraManager.SwitchCamera(cameraManager.dashCam);
            
        }
        }
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

        if(GameManager.Instance.doNothing == true)
        {
            dashing = false;
            dashLines.SetActive(false);
        }
        if(transform.position.y < -100 && health.currentHealth > 0)
        {
            health.Kill();
        }

         if(inText == false)
         {
            GameManager.Instance.doNothing = false;
         }

        if(GameManager.Instance.doNothing == true)
        {
            baseSpeed = 0.0f;
        }

        CollisionCheck();
        animator.SetBool("Grounded", rayGround);

        Vector3 deltaPosition = (transform.position - lastPosition) / Time.deltaTime;
        deltaPosition  = transform.InverseTransformDirection(deltaPosition);
        
        animator.SetFloat("Forward-back", deltaPosition.z * .1f);
        animator.SetFloat("side-to-side", deltaPosition.x * .1f);

        lastPosition = transform.position;
        
        UpdateSlopeSliding();

        if(inText == false)
        {
            nextLine = false;
        }

        
        if (!isGrounded && jumpCount == 0 )
        {
            
            if (jumpCount < jumpMax || isGrounded)
            {
                isJumping = false;
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
             speed = baseSpeed;
            
        }

        if (jumpCount == 1 )
        {
            velocity.y += gravityFirstJump * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            speed = airSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
        if (jumpCount == jumpMax)
        {
            velocity.y += gravitySecondJump * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            speed = airSpeed;
        }

        //Controller Check
         if(Gamepad.current == null)
        {
            hasGamePad = false;
        }
        else
        {
            hasGamePad = true;
        }
        
        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f && GameManager.Instance.doNothing == false)
        {
            animator.SetBool("Moving", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        gamepadMove.x = moveDir.x;
        gamepadMove.y = moveDir.y;

        
        //Dash
        if (Input.GetKeyDown(KeyCode.V))
        {
        GamepadDash();
        }
        Dashing();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        currectDashCoolDown -= Time.deltaTime;

        


       //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GamepadJump();
            NextLine();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
           nextLine = false;
        }
        
        //GroundCheck
        if(controller.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        groundedCheckDistence = (controller.height/2) + bufferCheckDistance;

        //lock on
        if (lockOn.target)
        {
            if  (lockOn.direction && Vector3.Distance(lockOn.target.transform.position, transform.position) < lockOn.attackRange*4)
            {

                if (lockOn.target == null){
                    lockOn.target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();
                }

                if (lockOn.target){ 
                    Vector3 dir = lockOn.target.transform.position - transform.position;
                    dir.y=0;
                    transform.rotation = Quaternion.LookRotation(dir);
                    
                }

                
            }
            if (lockOn.direction && Vector3.Distance(lockOn.target.transform.position, transform.position) > lockOn.attackRange * 4)
            {
                lockOn.LockOff();
            }
        }
        else
        {
            lockOn.FindNewTarget();
        }
    }

    // Sliding down slopes
    void UpdateSlopeSliding()
    {    
        var sphereCastVericalOffset = controller.height/2 - controller.radius;
        var castOrgin = transform.position - new Vector3(0,sphereCastVericalOffset,0);

        if(Physics.SphereCast(castOrgin, controller.radius - .01f, Vector3.down, out var hit, .05f, ~LayerMask.GetMask("Player"),QueryTriggerInteraction.Ignore))
        {

            var collider = hit.collider;
            var angle = Vector3.Angle(Vector3.up, hit.normal);

            if( angle > controller.slopeLimit)
            {
                velocity.x += slopeSpeed * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
            else
            {
                velocity.x = 0 *Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }            
        }
    }
    

    private void CollisionCheck()
    {
        bool lastraygrounded = rayGround;
        rayGround = false;
        Vector3 left = -transform.right * 0.5f;
        Vector3 right = transform.right * 0.5f;
        Vector3 back = -transform.forward * 0.5f;
        Vector3 forward = transform.forward * 0.5f;
        if (Physics.Linecast(transform.position+left, groundCheck.transform.position+left, out m_info, mask))
        {
            rayGround = true;
        }
        
        if (Physics.Linecast(transform.position+right, groundCheck.transform.position+right, out m_info, mask))
        {
            rayGround = true;
        }
        if (Physics.Linecast(transform.position+back, groundCheck.transform.position+back, out m_info, mask))
        {
            rayGround = true;
        }
        if (Physics.Linecast(transform.position+forward, groundCheck.transform.position+forward, out m_info, mask))
        {
            rayGround = true;
        }
        if (Physics.Linecast(transform.position, groundCheck.transform.position, out m_info, mask))
        {
            rayGround = true;
        }
        if(!lastraygrounded && rayGround == true)
        {
             gameObject.GetComponentInChildren<ParticleSystem>().Play();
             AudioManager.instance.PlayLandingSound();
        }
    }
}
