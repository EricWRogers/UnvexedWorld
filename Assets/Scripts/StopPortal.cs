using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class StopPortal : MonoBehaviour
{
    public Timer timer;
    public GameObject portal;

    public float timeRemaining = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        timer.StartTimer(timeRemaining, false);
    }

    public void TurnOffPortal()
    {
        portal.SetActive(false);
    }
}
