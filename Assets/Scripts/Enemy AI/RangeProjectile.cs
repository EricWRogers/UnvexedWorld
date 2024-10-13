using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectile : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody grenadePrefab;
    [SerializeField] 
    private Transform releasePosition;
    [SerializeField] 
    private float throwStrength = 10f;
    [SerializeField] 
    private float explosionDelay = 5f;
    [SerializeField] 
    private GameObject explosionEffect;

    [SerializeField] 
    private LineRenderer lineRenderer;
    [SerializeField] 
    private int linePoints = 25;
    [SerializeField] 
    private float timeBetweenPoints = 0.1f;
    [SerializeField] 
    private LayerMask grenadeCollisionMask;

    [SerializeField]
    private bool isThrowing = false;

    private void Update()
    {
        if (isThrowing) return;

        DrawProjection();
    }

    public void ThrowGrenade()
    {
        isThrowing = true;

        Rigidbody grenade = Instantiate(grenadePrefab, releasePosition.position, Quaternion.identity);
        grenade.AddForce(releasePosition.forward * throwStrength, ForceMode.Impulse);

        //StartCoroutine(ExplodeAfterDelay(grenade));
    }

    private IEnumerator ExplodeAfterDelay(Rigidbody grenade)
    {
        yield return new WaitForSeconds(explosionDelay);

        Instantiate(explosionEffect, grenade.transform.position, Quaternion.identity);
        Destroy(grenade.gameObject);

        isThrowing = false;
    }

    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;

        Vector3 startPosition = releasePosition.position;
        Vector3 startVelocity = throwStrength * releasePosition.forward;

        int i = 0;
        lineRenderer.SetPosition(i, startPosition);

        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit,
                (point - lastPosition).magnitude, grenadeCollisionMask))
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }
    }
}
