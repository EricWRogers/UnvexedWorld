using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private GameObject target;
    void Start()
    {
        // If TargetingSystem is found call FindTarget
        target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();

        if (target)
            gameObject.transform.LookAt(target.transform);
    }

    void Update()
    {
        if (target)
        {
            gameObject.transform.LookAt(target.transform);
        }else
        {
            target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();
            if(target)
            {
                gameObject.transform.LookAt(target.transform);
            }
        }
    }
}
