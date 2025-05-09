using UnityEngine;
using Cinemachine;
using System.Collections;
using System;

public class ObjectiveCameraSwitch : MonoBehaviour
{
    public CameraManager camMan;

    public CinemachineBrain brain;

    public float tranTime;

    public bool goBack = false;

     public bool textBased = false;

     public ThirdPersonMovement movement;

     public bool once = false;

      public bool noRepeat = false;

     public float returnTime;

     public PauseMenu pauseMenu;

     public GameObject currentObject;

     public CinemachineVirtualCamera objCam;

     public String Key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camMan = FindFirstObjectByType<CameraManager>();

         movement = FindFirstObjectByType<ThirdPersonMovement>();

         pauseMenu = FindFirstObjectByType<PauseMenu>();

         if(GameManager.Instance.switches.Contains(Key))
         {
            gameObject.SetActive(false);
         }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        objCam.LookAt = currentObject.transform;
        objCam.Follow = currentObject.transform;
        GameManager.Instance.switches.Add(Key);
        if (other.gameObject.tag == "Player" && noRepeat == false)
        {
            noRepeat = true;
            if(GameManager.Instance.doNothing == true)
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
        if(GameManager.Instance.doNothing == true && pauseMenu.isPaused == false)
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
        
        yield return new WaitForSecondsRealtime(returnTime);
        brain.m_DefaultBlend.m_Time = 0.2f;
        camMan.dontChange = false;
        if(GameManager.Instance.doNothing == false)
            {
                brain.m_IgnoreTimeScale = false;

            }

        if(once == true){
            Destroy(this);
        }
    }

    public void CamForEmpty()
    {
        
        camMan.OBJCamera();
        StartCoroutine(ReturnCamera());
         objCam.LookAt = currentObject.transform;
        objCam.Follow = currentObject.transform;
    }
    
}
