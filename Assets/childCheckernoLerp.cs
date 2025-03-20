using UnityEngine;

public class childCheckernoLerp : MonoBehaviour
{
    public ObjectiveCameraSwitch objCam;
      bool callOnce = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            if (callOnce == false)
            {
                objCam.CamForEmpty();
                callOnce = true;
            }
        }
    }
}
