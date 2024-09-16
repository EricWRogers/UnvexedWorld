using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public float distanceFromPlayer = 5;
   
    

    
    void Update()
    {
        transform.position = player.transform.position - player.transform.forward * distanceFromPlayer;
        transform.LookAt(player.transform.position);
    }
}
