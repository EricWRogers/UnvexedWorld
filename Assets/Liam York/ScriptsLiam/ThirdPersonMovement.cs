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

    public float turnSmoothTime = 0.1f;

    public bool isGrounded = false;

    public bool isJumping;

    public int jumpCount = 0;

    public int jumpMax = 2;

    public float jumpForce = 5f;

    float turnSmoothVelocity;

    Vector3 velocity;

    public Transform cam;

    public Vector3 moveDir;



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
        if (Input.GetKeyDown(KeyCode.LeftShift) && (isGrounded))
        {


            StartCoroutine(Dash());
        }

        IEnumerator Dash()
        {
            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
               controller.Move( moveDir * dashSpeed *Time.deltaTime);

                yield return null;
            }

        }


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
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("Ground")&& !isGrounded)
        {
            isGrounded = true;
            Debug.Log("Ouch");
            jumpCount = 0;
        }
    }
}
