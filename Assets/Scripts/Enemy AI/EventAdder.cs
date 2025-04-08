using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

public class EventAdder : MonoBehaviour
{

    public MeleeRangedAttack MRA;
    public CameraLockon CL;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        MRA = GameObject.FindFirstObjectByType<MeleeRangedAttack>();
        CL = GameObject.FindFirstObjectByType<CameraLockon>();
        gameObject.GetComponent<Health>()?.outOfHealth.AddListener(MRA.LockOffEvent);
        gameObject.GetComponent<Health>()?.outOfHealth.AddListener(CL.Remove);
        gameObject.GetComponent<Health>()?.hurt.AddListener(AudioManager.instance.PlayEnemyHurtSound);
    }

    // Update is called once per frame
    void Update()
    {
         

        
    }
}
