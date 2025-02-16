using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLockon : MonoBehaviour
{
    public CinemachineFreeLook lockCamera;
    private MeleeRangedAttack lockOn;
    private GameObject player;
    public CinemachineTargetGroup tg;
    public Transform groupObject;
    public bool oneTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lockOn = GameObject.FindFirstObjectByType<MeleeRangedAttack>();

        tg = GameObject.FindFirstObjectByType<CinemachineTargetGroup>();

        groupObject = GameObject.FindGameObjectWithTag("ObjectGroup").transform;
    }

    // Update is called once per frame
    void Update()
    {
        AddM();

        if (lockOn.target && lockOn.direction == true)
        {
            if (lockOn.direction && Vector3.Distance(lockOn.target.transform.position, transform.position) < lockOn.attackRange * 3)
            {
                

                
                lockCamera.LookAt = groupObject;

            }
        }

        if (lockOn.direction == false)
       {
            lockCamera.LookAt = player.transform;
            tg.RemoveMember(lockOn.target.transform);
            tg.RemoveMember(player.transform);
        }

      
        
    }

    void AddM()
    {
        if (lockOn.target && lockOn.direction == true && oneTime == true)
        {
            if (lockOn.direction && Vector3.Distance(lockOn.target.transform.position, transform.position) < lockOn.attackRange * 3)
            {
                
                    tg.AddMember(lockOn.target.transform, 1, 10);
                    tg.AddMember(player.transform, 3, 2);
                    oneTime = false;




            }
        }
        
    }
}
