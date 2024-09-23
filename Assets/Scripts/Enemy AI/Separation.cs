using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour
{
    [SerializeField] private float separationRadius = 2.0f;  // Distance to keep from other AI
    [SerializeField] private float separationStrength = 5.0f; // How strong the separation force is
    [SerializeField] private LayerMask aiMask;  // Layer mask to identify other AI

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        Vector3 separationForce = CalculateSeparationForce();
        agent.Move(separationForce * Time.deltaTime);  // Apply the separation force to the movement
    }

    // Calculate the separation force based on nearby AI units
    private Vector3 CalculateSeparationForce()
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, separationRadius, aiMask);
        Vector3 separation = Vector3.zero;
        int nearbyCount = 0;

        foreach (Collider enemy in nearbyEnemies)
        {
            if (enemy != null && enemy.gameObject != this.gameObject)
            {
                Vector3 directionAway = transform.position - enemy.transform.position;
                separation += directionAway.normalized / directionAway.magnitude;
                nearbyCount++;
            }
        }

        if (nearbyCount > 0)
        {
            separation /= nearbyCount;  // Average the separation force
            separation *= separationStrength;  // Apply strength modifier
        }

        return separation;
    }
}
