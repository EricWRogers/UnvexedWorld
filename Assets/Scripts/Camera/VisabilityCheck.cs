using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class VisabilityCheck : MonoBehaviour
{
    public Camera mainCamera;
    public MeleeRangedAttack lockOn;

    public CinemachineFreeLook thisView;

    public GameObject player;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockOn = GameObject.FindFirstObjectByType<MeleeRangedAttack>();

        mainCamera = Camera.main;

        player = GameObject.FindGameObjectWithTag("Player");

       
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible() && lockOn.direction == true)
        {
          
        }
        else
        {
            
            if(lockOn.direction == true){
                thisView.m_RecenterToTargetHeading.m_enabled = true;
            
            }
            else
            {
                thisView.m_RecenterToTargetHeading.m_enabled = false;
            }
        }

        if(playerIsVisible() && lockOn.direction == true)
        {
          
        }
        else
        {
            
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
        if(lockOn.target == null){
            return false;
        }
        return planes.All(plane => plane.GetDistanceToPoint(lockOn.target.transform.position) >= 0);
        
        
    }
     private bool playerIsVisible()
     {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return planes.All(plane => plane.GetDistanceToPoint(player.transform.position) >= 0);
     }

    
}
