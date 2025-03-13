using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

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

        if (lockOn.direction == false && lockOn.target != null)
       {
            lockCamera.LookAt = player.transform;
            
            tg.m_Targets = new CinemachineTargetGroup.Target[0];
        }

      
        
    }

    public void Remove()
    {
        
        tg.m_Targets = new CinemachineTargetGroup.Target[0];
    }

    void AddM()
    {
        if (lockOn.target && lockOn.direction == true && oneTime == true)
        {
            if (lockOn.direction && Vector3.Distance(lockOn.target.transform.position, transform.position) < lockOn.attackRange * 4)
            {
                
                    tg.AddMember(lockOn.target.transform, 5, 15);
                    tg.AddMember(player.transform, 10, 20);
                    oneTime = false;




            }
        }
        
    }
}
