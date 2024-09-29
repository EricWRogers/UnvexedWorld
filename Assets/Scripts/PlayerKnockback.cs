using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrength = 5.0f;
    [SerializeField]
    private float knockbackDuration = 0.2f;
    private CharacterController controller;
    private Vector3 knockbackDirection;
    private float knockbackTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Call this when the player is hurt
    public void ApplyKnockback(Vector3 hitDirection)
    {
        knockbackDirection = hitDirection.normalized * knockbackStrength;
        knockbackTimer = knockbackDuration;
    }

    void Update()
    {
        if (knockbackTimer > 0)
        {
            // Apply knockback force smoothly over time
            controller.Move(knockbackDirection * Time.deltaTime);

            // Gradually reduce the knockback effect (lerp to zero)
            knockbackDirection = Vector3.Lerp(knockbackDirection, Vector3.zero, Time.deltaTime / knockbackDuration);
            
            knockbackTimer -= Time.deltaTime;
        }
    }
}
