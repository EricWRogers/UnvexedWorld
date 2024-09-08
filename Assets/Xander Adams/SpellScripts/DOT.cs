using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class DOT : MonoBehaviour
{
    // Start is called before the first frame update

    public Timer timer;
    public Health health;
    private void Start()
    {
        gameObject.TryGetComponent<Health>(out health);
        if(health == null)
        {
            Destroy(gameObject.GetComponent<DOT>());
        }
        timer = gameObject.AddComponent<Timer>();
        timer.StartTimerFromEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
