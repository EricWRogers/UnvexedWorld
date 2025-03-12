using UnityEngine;
using Cinemachine;
using System.Collections;

public class ObjectiveCameraSwitch : MonoBehaviour
{
    public CameraManager camMan;

    public CinemachineBrain brain;

    public float tranTime;

    public bool goBack = false;

     public float returnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             
            brain.m_DefaultBlend.m_Time = tranTime;

                camMan.OBJCamera();

                if(goBack == true)
                {
                    StartCoroutine(ReturnCamera());
                }
            
              
        }   
    }

     IEnumerator ReturnCamera()
    {
        yield return new WaitForSeconds(returnTime);
        camMan.ReturnCamera();
    }
}
