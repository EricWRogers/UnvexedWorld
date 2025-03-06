using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitStop : MonoBehaviour
{

    public bool waiting;

    public PauseMenu gamePaused;

    public float scaleTime = 0.0f;

    private bool isSlow = false;


    public void Start()
    {
        gamePaused = FindFirstObjectByType<PauseMenu>();
    }
    public void Stop(float duration)
    {
        if (duration == 0.0f)
            return;
        if(isSlow)
            return;
            isSlow = true;
        if (waiting)
            return;
        Time.timeScale = scaleTime;
        StopAllCoroutines();
        StartCoroutine(Wait(duration));
    }

    void OnDestroy()
    {
        StopAllCoroutines();

        if (gamePaused != null)
        {
            if(gamePaused.isPaused == false)
            {
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        
        waiting = false;
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
