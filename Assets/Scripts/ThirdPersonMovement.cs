using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    public float gravity = -3.5f;

    public float speed = 6f;

    public float dashSpeed = 10f;

    public float dashTime = 0.75f;

    public float dashCoolDown = 3.0f;
   
    public float currectDashCoolDown = 0.0f;

    public float turnSmoothTime = 0.1f;

    public bool dashing = false;

    public bool isGrounded = false;

    public float groundCheckDistance;

    private float bufferCheckDistance = 0.1f;

    public bool isJumping;

    public int jumpCount = 0;

    public int jumpMax = 2;

    public float jumpForce = 5f;

    float turnSmoothVelocity;

    Vector3 velocity;

    public Transform cam;

    public Vector3 moveDir;

    private float dashStartTime;

    public GameObject groundCheck;

     public float m_MaxDistance;

    bool m_HitDetect;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
         
        
    }



    // Update is called once per frame
    void Update()
    {
        
        if (!isGrounded)
        {
            
            if (jumpCount < jumpMax)
            {
                isJumping = false;
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            
        }
        
        //Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

             moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            


        }
        //Dash
        currectDashCoolDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift)&& (!dashing) && currectDashCoolDown <= 0.0f)
        {

            dashing = true;
            dashStartTime = Time.time;
           
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (dashing)
        {
            if (Time.time < dashStartTime + dashTime)
            {
                controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            }
            else
            {
                dashing = false;
                currectDashCoolDown = dashCoolDown;
            }
        }


        groundCheckDistance = (controller.height / 2) + bufferCheckDistance;
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < jumpMax))
        {
            isJumping = true;
            jumpCount++;
            isGrounded = false;
            velocity.y = 0;
            velocity.y += jumpForce;
            controller.Move(velocity * Time.deltaTime);

        }
        

        if(controller.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            isGrounded = true;
            Debug.Log("ouch");
        }
        else
        {
            isGrounded = false;
        }

 
        


    }
    
    
    public void StopMoving()
    {
        Destroy(this);
    }

  
}
