using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

public class EventAdder : MonoBehaviour
{

    public MeleeRangedAttack MRA;
    public CameraLockon CL;
    public bool isBoss = false;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        MRA = GameObject.FindFirstObjectByType<MeleeRangedAttack>();
        CL = GameObject.FindFirstObjectByType<CameraLockon>();
        gameObject.GetComponent<Health>()?.outOfHealth.AddListener(MRA.LockOffEvent);
        gameObject.GetComponent<Health>()?.outOfHealth.AddListener(CL.Remove);
        if(isBoss)
        {
            Debug.Log("Dummy we dont have a boss yet");
            //gameObject.GetComponent<Health>()?.outOfHealth.AddListener(AudioManager.instance.PlayBossRoarSound);
        }else
        {
            gameObject.GetComponent<Health>()?.outOfHealth.AddListener(AudioManager.instance.PlayEnemyDeathSound);
            gameObject.GetComponent<Health>()?.hurt.AddListener(AudioManager.instance.PlayEnemyHurtSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
         

        
    }
}
