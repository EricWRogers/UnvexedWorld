using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

public class Crystalize : MonoBehaviour
{
    public Timer timer;
    public int shatterDamage = 10;
    public float crystalization = 5;
    public bool crystalized;
    public int decayRate = 1;
    public int duration = 5;
    public GameObject particle;
    public GameObject fullCrystal;
    public Health health;
    public MessageSpawner messageSpawner; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.TryGetComponent<Health>(out health);
        particle = Instantiate(particle, transform.position, Quaternion.Euler(transform.rotation.x - 90, transform.rotation.y, transform.rotation.z));
        particle.transform.parent = gameObject.transform;

        if (health == null)
        {
            Destroy(gameObject.GetComponent<Crystalize>());
            return;
        }

        //messageSpawner = gameObject.GetComponentInChildren<MessageSpawner>();

        timer = gameObject.AddComponent<Timer>();
        if (timer.timeout == null)
            timer.timeout = new UnityEvent();

        timer.countDownTime = duration;
        timer.autoRestart = true;
        timer.autoStart = false;
        timer.timeout.AddListener(RemoveCrystalize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        crystalization -= decayRate*Time.deltaTime;
        if(crystalization<=0)
        {
            RemoveCrystalize();
        }
        else if (crystalization>=100)
        {
            if (!crystalized)
            {
                fullCrystal = Instantiate(fullCrystal, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
                fullCrystal.transform.parent = gameObject.transform;
                crystalized=true;
                timer.StartTimer();
            }
        }
    }
    public void RemoveCrystalize()
    {
        Destroy(timer);
        Destroy(particle);
        if(crystalized)
        {
            Shatter();
        }
        Destroy(gameObject.GetComponent<Crystalize>());
    }

    public void Shatter()
    {
        Destroy(fullCrystal);
        health.Damage(shatterDamage);
    }
}
