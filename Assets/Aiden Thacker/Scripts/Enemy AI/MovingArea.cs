using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArea : MonoBehaviour
{
    public float area = 5.0f;
    
    #if (UNITY_EDITOR)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, area);
    }
    #endif
}
