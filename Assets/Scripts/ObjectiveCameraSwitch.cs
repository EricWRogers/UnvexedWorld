using UnityEngine;
using Cinemachine;
using System.Collections;

public class ObjectiveCameraSwitch : MonoBehaviour
{
    public CameraManager camMan;

    public CinemachineBrain brain;

    public float tranTime;

    public bool goBack = false;

     public bool textBased = false;

     public ThirdPersonMovement movement;

     public bool once = false;

     public float returnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camMan = FindFirstObjectByType<CameraManager>();

         movement = FindFirstObjectByType<ThirdPersonMovement>();
       

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
                if( textBased == true )
                {
                    if(movement.inText == false){
                    StartCoroutine(ReturnCamera());
                    }
                }
            
            
              
        }   
    }

     IEnumerator ReturnCamera()
    {
        
        yield return new WaitForSecondsRealtime(returnTime);
        if(textBased == true)
        {
            while(movement.inText == true)
            {
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        camMan.backCamera();
        yield return new WaitForSecondsRealtime(returnTime);
        brain.m_DefaultBlend.m_Time = 0.2f;

        if(once == true){
            Destroy(this);
        }
    }
    
}
