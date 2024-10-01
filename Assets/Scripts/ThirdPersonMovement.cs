using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    PlayerGamepad gamepad;

    Vector2 gamepadMove;

    public CameraManager cameraManager;
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

    public float attackForwared = 2.0f;

    void Awake()
    {
        gamepad = new PlayerGamepad();

        gamepad.GamePlay.Jump.performed += ctx => GamepadJump();

        gamepad.GamePlay.Dash.performed += ctx => GamepadDash();

        gamepad.GamePlay.Movement.performed += ctx => gamepadMove = ctx.ReadValue<Vector2>();
        gamepad.GamePlay.Movement.canceled += ctx => gamepadMove = Vector2.zero;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < jumpMax))
        {
            isJumping = true;
            jumpCount++;
            isGrounded = false;
            velocity.y = 0;
            velocity.y += jumpForce;
            controller.Move(velocity * Time.deltaTime);

        }
    }

    void GamepadJump()
    {
        if ((isGrounded || jumpCount < jumpMax))
        {
            isJumping = true;
            jumpCount++;
            isGrounded = false;
            velocity.y = 0;
            velocity.y += jumpForce;
            controller.Move(velocity * Time.deltaTime);

        }
    }

    void Dash()
    {
         currectDashCoolDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift)&& (!dashing) && currectDashCoolDown <= 0.0f)
        {

            dashing = true;
            dashStartTime = Time.time;
            cameraManager.SwitchCamera(cameraManager.dashCam);
            Vector3 dir = (transform.position - cam.transform.position).normalized;
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
        }
        

        if (dashing)
        {
            if (Time.time < dashStartTime + dashTime)
            {
                controller.Move(cam.forward * dashSpeed * Time.deltaTime);
            }
            else
            {
                dashing = false;
                currectDashCoolDown = dashCoolDown;
                cameraManager.SwitchCamera(cameraManager.mainCam);
            }
        }

    }

    void GamepadDash()
    {
         currectDashCoolDown -= Time.deltaTime;

        if ( (!dashing) && currectDashCoolDown <= 0.0f)
        {

            dashing = true;
            dashStartTime = Time.time;
            cameraManager.SwitchCamera(cameraManager.dashCam);
            Vector3 dir = (transform.position - cam.transform.position).normalized;
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
        }
        

        if (dashing)
        {
            if (Time.time < dashStartTime + dashTime)
            {
                controller.Move(cam.forward * dashSpeed * Time.deltaTime);
            }
            else
            {
                dashing = false;
                currectDashCoolDown = dashCoolDown;
                cameraManager.SwitchCamera(cameraManager.mainCam);
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
    
    void Start()
    {
        animator = GetComponentsInChildren<Animator>()[1];
        Cursor.lockState = CursorLockMode.Locked;    
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", rayGround);
        
        UpdateSlopeSliding();

        
        if (!isGrounded && jumpCount == 0)
        {
            
            if (jumpCount < jumpMax || isGrounded)
            {
                isJumping = false;
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
             speed = baseSpeed;
            
        }

        if (jumpCount == 1)
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
        
        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
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
        Dash();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        
        


       //Jump
        Jump();
        
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

        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up, out hit,groundedCheckDistence))
        {
            rayGround = true;
        }
        else
        {
            rayGround = false;
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
    
    public void StopMoving()
    {
        Destroy(this);
    }  

    public void Attackforward()
    {
         velocity.x -= attackForwared * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

 
}
