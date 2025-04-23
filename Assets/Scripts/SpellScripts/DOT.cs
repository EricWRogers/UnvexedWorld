using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

public class DOT : MonoBehaviour
{
    public Timer timer;
    public float damageTimer = 0;
    public float damageRate = 0.25f;
    public int tickDamage = 1;
    public int duration = 10;
    public GameObject particle;
    public Health health;
    public MessageSpawner messageSpawner; 

    private void Start()
    {
        gameObject.TryGetComponent<Health>(out health);
        particle = Instantiate(particle, transform.position, Quaternion.Euler(transform.rotation.x - 90, transform.rotation.y, transform.rotation.z));
        particle.transform.parent = gameObject.transform;

        if (health == null)
        {
            Destroy(gameObject.GetComponent<DOT>());
            return;
        }

        //messageSpawner = gameObject.GetComponentInChildren<MessageSpawner>();

        timer = gameObject.AddComponent<Timer>();
        if (timer.timeout == null)
            timer.timeout = new UnityEvent();

        timer.countDownTime = duration;
        timer.autoRestart = true;
        timer.autoStart = true;
        timer.timeout.AddListener(RemoveDOT);
    }

    void FixedUpdate()
    {
        damageTimer += Time.deltaTime;

        if (damageTimer > damageRate)
        {
            health.Damage(tickDamage);
            MessageSpawner messageSpawner = gameObject.GetComponentInChildren<MessageSpawner>();
            if (messageSpawner != null)
            {
                messageSpawner.ApplyDamage(gameObject);
            }
            else
            {
                Debug.Log("MessageSpawner not found on the target." + gameObject.name );
            }
            damageTimer -= damageRate;
        }
    }

    public void RemoveDOT()
    {
        Destroy(timer);
        Destroy(particle);
        Destroy(gameObject.GetComponent<DOT>());
    }

    public int GetDamage()
    {
        return tickDamage;
    }
}
