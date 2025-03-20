using System.Net;
using UnityEngine;

public class childChecker : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;

    public ObjectiveCameraSwitch objCam;

    public bool begin = false;

     private float desiredDuration = 10f;
    private float elapsedTime;

    [SerializeField]
    private AnimationCurve curve;

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
            begin = true;
            if(begin == true){
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;
    
            StartPoint.transform.position = Vector3.Lerp(StartPoint.transform.position, EndPoint.transform.position, curve.Evaluate(percentageComplete));
            }
        }
            
        }
}

    

