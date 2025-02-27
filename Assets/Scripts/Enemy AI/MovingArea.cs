using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArea : MonoBehaviour
{
    public float area = 5.0f;
    public float width, height, length;
    public bool isSquare;
    public BoxCollider box;
    public SphereCollider sphere;
    
    #if (UNITY_EDITOR)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(isSquare)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, length));
            box.enabled = true;
            sphere.enabled = false;
            box.size = new Vector3(width, height, length);
        }else
        {
            Gizmos.DrawWireSphere(transform.position, area);
            sphere.enabled = true;
            box.enabled = false;  
            sphere.radius = area;  
        }
    }
    #endif
}
