using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

public class MeleeStateMachine : SimpleStateMachine
{
    // private MeleeState melee;
    // private InRangeState inRange;

    // public bool LOS;
    // public bool isAlive;
    // public float radius;
    // public int dmg;
    // private Health health;
    // private Transform target;

    // void Awake()
    // {
    //     states.Add(moveInRange);
    //     states.Add(Charge);

    //     foreach (SimpleState s in states)
    //     {
    //         s.stateMachine = this;
    //     }
    // }

    // void Start()
    // {
    //     health = GameObject.GetComponent<Health>();
        
    //     target = GameObject.FindGameObjectWithTag("Player").transform;

    //     ChangeState(nameof(InRangeState));
    // }

    // void Update()
    // {
    //     if(health.currentHealth > 0)
    //     {
    //         isAlive = true;
    //     }else
    //     {
    //         isAlive = false;
    //     }
    //     //LOS
    // }
}
