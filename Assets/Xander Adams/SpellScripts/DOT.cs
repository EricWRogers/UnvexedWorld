using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;

public class DOT : MonoBehaviour
{
    // Start is called before the first frame update

    public Timer timer;
    public float damageTimer = 0;
    public float damageRate = .25f;
    public int tickDamage = 1;
    public int duration = 10;
    public Health health;
    private void Start()
    {
        gameObject.TryGetComponent<Health>(out health);
        if(health == null)
        {
            Destroy(gameObject.GetComponent<DOT>());
        }
        timer = gameObject.AddComponent<Timer>();
        if (timer.timeout == null)
                timer.timeout = new UnityEvent();
        timer.countDownTime = duration;
        timer.autoRestart = true;
        timer.autoStart = true;
        timer.timeout.AddListener(RemoveDOT);
        //timer.StartTimerFromEvent();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        damageTimer += Time.deltaTime;

        if (damageTimer > damageRate)
        {
            health.currentHealth -= tickDamage;
            damageTimer -= damageRate;
        }
    }

    public void RemoveDOT()
    {
        Destroy(timer);
        Destroy(gameObject.GetComponent<DOT>());
    }
}
