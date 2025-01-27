using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using UnityEngine.AI;

[System.Serializable]
public class FleeState : SimpleState
{
    // NavMeshAgent agent;
    // Transform agentTransform;
    // private float pos;

    // public override void OnStart()
    // {
    //     Debug.Log("Flee State");
    //     base.OnStart();
    //     pos = Random.Range(((MeleeStateMachine)stateMachine).ranMinFlee, ((MeleeStateMachine)stateMachine).ranMaxFlee);
    //     agent = ((MeleeStateMachine)stateMachine).GetComponent<NavMeshAgent>();

    //     RunAway();

    // }
    // public override void UpdateState(float _dt)
    // {
    //     base.UpdateState(_dt);

    //     if(agent.remainingDistance <= 0)
    //     {
    //         stateMachine.ChangeState(nameof(InRangeState));
    //     }


    // }
    // void RunAway()
    // {
    //     agentTransform = ((MeleeStateMachine)stateMachine).transform;

    //     ((MeleeStateMachine)stateMachine).transform.rotation = Quaternion.LookRotation(((MeleeStateMachine)stateMachine).transform.position - ((MeleeStateMachine)stateMachine).target.position);

    //     Vector3 fleePos = ((MeleeStateMachine)stateMachine).transform.position + ((MeleeStateMachine)stateMachine).transform.forward * pos; 

    //     NavMeshHit hit;

    //     NavMesh.SamplePosition(fleePos, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));

    //     ((MeleeStateMachine)stateMachine).transform.position = agentTransform.position;
    //     ((MeleeStateMachine)stateMachine).transform.rotation = agentTransform.rotation;

    //     agent.SetDestination(hit.position);

    // }
}
