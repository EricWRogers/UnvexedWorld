using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;

public class EnemyFinder : MonoBehaviour
{
    public List<SimpleStateMachine> nearbyEnemies = new List<SimpleStateMachine>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GroundEnemy") || other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<GruntStateMachine>() != null)
            {
                var gruntStateMachine = other.GetComponent<GruntStateMachine>();
                nearbyEnemies.Add(gruntStateMachine);
            }
        }
    }
}
