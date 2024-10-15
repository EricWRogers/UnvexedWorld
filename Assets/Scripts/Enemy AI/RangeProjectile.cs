using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectile : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody prefab;
    [SerializeField] 
    private Transform firePoint;
    [SerializeField] 
    private float throwStrength = 10f;

    [SerializeField] 
    private LineRenderer lineRenderer;
    [SerializeField] 
    private int linePoints = 25;
    [SerializeField] 
    private float timeBetweenPoints = 0.1f;
    [SerializeField] 
    private LayerMask collisionMask;

    [SerializeField]
    private bool isThrowing = false;

    private void Update()
    {
        if (isThrowing) return;

        DrawProjection();
    }

    public void ThrowProjectile()
    {
        isThrowing = true;

        Rigidbody projectile = Instantiate(prefab, firePoint.position, Quaternion.identity);
        projectile.AddForce(firePoint.forward * throwStrength, ForceMode.Impulse);
    }

    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;

        Vector3 startPosition = firePoint.position;
        Vector3 startVelocity = throwStrength * firePoint.forward;

        int i = 0;
        lineRenderer.SetPosition(i, startPosition);

        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit,(point - lastPosition).magnitude, collisionMask))
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }
    }
}
