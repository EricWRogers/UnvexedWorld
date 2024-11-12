using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbTele : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
