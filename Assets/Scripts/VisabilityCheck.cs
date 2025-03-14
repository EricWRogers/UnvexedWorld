using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class VisabilityCheck : MonoBehaviour
{
    public Camera mainCamera;
    public MeleeRangedAttack lockOn;

    public CinemachineFreeLook thisView;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockOn = GameObject.FindFirstObjectByType<MeleeRangedAttack>();

        mainCamera = Camera.main;

       
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible() && lockOn.direction == true)
        {
            Debug.Log("fr");
        }
        else
        {
            Debug.Log("NOWAY");
            if(lockOn.direction == true){
                thisView.m_RecenterToTargetHeading.m_enabled = true;
            
            }
            else
            {
                thisView.m_RecenterToTargetHeading.m_enabled = false;
            }
        }
    }

    private bool isVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return planes.All(plane => plane.GetDistanceToPoint(lockOn.target.transform.position) >= 0);
    }

    
}
