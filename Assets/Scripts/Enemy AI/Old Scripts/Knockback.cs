using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    public float knockbackStrength = 5.0f;
    public float knockbackDuration = 0.2f;
    private Rigidbody rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnHurt()
    {
        Vector3 hitDirection = transform.position - player.position;
        ApplyKnockback(hitDirection);
    }

    public void ApplyKnockback(Vector3 hitDirection)
    {
        if (rb != null)
        {
           // rb.isKinematic = false;
            Vector3 force = hitDirection.normalized * knockbackStrength;
            rb.AddForce(force, ForceMode.Impulse);
            StartCoroutine(ResetKinematicAfterDelay());
        }
    }

    private IEnumerator ResetKinematicAfterDelay()
    {
        yield return new WaitForSeconds(knockbackDuration);

        //rb.isKinematic = true;
    }
}
