using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitStop : MonoBehaviour
{

    public bool waiting;

    public PauseMenu gamePaused;

    public float scaleTime = 0.0f;


    public void Start()
    {
        gamePaused = FindFirstObjectByType<PauseMenu>();
    }
    public void Stop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = scaleTime;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        if(gamePaused.isPaused == false)
        {
            Time.timeScale = 1.0f;
        }
        waiting = false;

    }
}
