using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    

    public float dashSpeed = 10.0f;

    public float dashTime = 0.75f;


    

    ThirdPersonMovement moveScript;

    void Start()
    {
        moveScript = GetComponent<ThirdPersonMovement>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) &&( moveScript.isGrounded))
        {


            StartCoroutine(Dash());
        }

        IEnumerator Dash()
        {
            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

                yield return null;
            }

        }
        


    }
}   
