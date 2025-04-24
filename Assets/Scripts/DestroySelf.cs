using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;

public class DestroySelf : MonoBehaviour
{
    public Timer timer;
    public float duration = 10;
    public bool onStart = true;

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        if (timer.timeout == null)
            timer.timeout = new UnityEvent();

        timer.countDownTime = duration;
        timer.autoRestart = true;
        timer.autoStart = onStart;
        timer.timeout.AddListener(End);
    }
    public void End()
    {
        Destroy(gameObject);
    }

    public void Activate()
    {
        timer.StartTimer();
    }
}
