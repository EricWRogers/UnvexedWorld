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

     public PauseMenu pauseMenu;

     public GameObject currentObject;

     public CinemachineVirtualCamera objCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camMan = FindFirstObjectByType<CameraManager>();

         movement = FindFirstObjectByType<ThirdPersonMovement>();

         pauseMenu = FindFirstObjectByType<PauseMenu>();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        objCam.LookAt = currentObject.transform;
        objCam.Follow = currentObject.transform;
        if (other.gameObject.tag == "Player")
        {
            if(GameManager.instance.doNothing == true)
            {
                brain.m_IgnoreTimeScale = true;

            }
           

             
            
            
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

     public IEnumerator ReturnCamera()
    {
        if(GameManager.instance.doNothing == true && pauseMenu.isPaused == false)
            {
                brain.m_IgnoreTimeScale = true;

            }
        camMan.dontChange = true;
        yield return new WaitForSecondsRealtime(returnTime);
        if(textBased == true)
        {
            while(movement.inText == true)
            {
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        camMan.backCamera();
        Debug.Log("go back");
        yield return new WaitForSecondsRealtime(returnTime);
        brain.m_DefaultBlend.m_Time = 0.2f;
        camMan.dontChange = false;
        if(GameManager.instance.doNothing == false)
            {
                brain.m_IgnoreTimeScale = false;

            }

        if(once == true){
            Destroy(this);
        }
    }

    public void CamForEmpty()
    {
        Debug.Log("called");
        camMan.OBJCamera();
        StartCoroutine(ReturnCamera());
    }
    
}
