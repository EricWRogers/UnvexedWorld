using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
    public bool targetsInSight;

    void FixedUpdate()
    {
        FindVisibleTargets();
    }
    
    public void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add (target);
                }
            }
        }
        if (visibleTargets.Count > 0)
        {
            targetsInSight = true;
        }
        else
        {
            targetsInSight = false;
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees* Mathf.Deg2Rad));
    }
}
