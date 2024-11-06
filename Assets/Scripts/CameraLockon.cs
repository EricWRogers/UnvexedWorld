using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLockon : MonoBehaviour
{
    public CinemachineFreeLook lockCamera;
    private MeleeRangedAttack lockOn;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lockOn = GameObject.FindObjectOfType<MeleeRangedAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lockOn.target && lockOn.direction == true)
        {
            if(lockOn.direction && Vector3.Distance(lockOn.target.transform.position, transform.position) < lockOn.attackRange*3)
            {
                lockCamera.LookAt = lockOn.target.transform;
             
            }
        }
       if(lockOn.direction == false)
       {
            lockCamera.LookAt = player.transform;
       }
        
    }
}
