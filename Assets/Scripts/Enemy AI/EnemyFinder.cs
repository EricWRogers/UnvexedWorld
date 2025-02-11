using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;

public class EnemyFinder : MonoBehaviour
{
    public List<SimpleStateMachine> nearbyEnemies = new List<SimpleStateMachine>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SimpleStateMachine enemy))
        {
            if (!nearbyEnemies.Contains(enemy))
            {
                nearbyEnemies.Add(enemy);
            }
        }
    }
}
