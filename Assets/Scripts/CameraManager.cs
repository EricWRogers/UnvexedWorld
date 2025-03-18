using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineFreeLook[] cameras;

    public CinemachineFreeLook mainCam;
    public CinemachineFreeLook dashCam;
    public CinemachineFreeLook startCamera;

    public  CinemachineFreeLook meleeCamera;
    public CinemachineFreeLook currentCam;

    public CinemachineVirtualCamera ObjCam;

    public bool dontChange = false;
    

    private void Start()
    {
        currentCam = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
                    
        }
    }
    public void SwitchCamera(CinemachineFreeLook newCam)
    {
        if(dontChange == false){
            currentCam = newCam;

            currentCam.Priority = 20;

            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != currentCam)
                {
                    cameras[i].Priority = 10;
                }
            }
        }
    }

    public void OBJCamera()
    {
        currentCam.Priority = 10;
         ObjCam.Priority = 20;
         Debug.Log("OBJCamera");
         GameManager.instance.doNothing = true;

    }

   public void backCamera()
   {
        currentCam.Priority = 20;
        ObjCam.Priority = 10;
        Debug.Log("Back to you");
        GameManager.instance.doNothing = false;
   }
}
