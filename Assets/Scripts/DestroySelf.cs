using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;

public class DestroySelf : MonoBehaviour
{
    public Timer timer;
    public float duration = 10;

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        if (timer.timeout == null)
            timer.timeout = new UnityEvent();

        timer.countDownTime = duration;
        timer.autoRestart = true;
        timer.autoStart = true;
        timer.timeout.AddListener(End);
    }
    public void End()
    {
        Destroy(gameObject);
    }
}
