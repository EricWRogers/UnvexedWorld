using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

public class DeadlyFog : MonoBehaviour
{
    //public Timer timer;
    public Health playerHealth;

    public int damage = 1;
    public bool inFog = false;

    public float damageTimer = 0;
    public float damageRate = 0.25f;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void DealDamage()
    {
        Debug.Log("Player is Damaged");
        playerHealth.Damage(damage);
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inFog = true;
            Debug.Log("This is the " + col.gameObject.name);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            inFog = false;
        }
    }

    void Update()
    {
        if (inFog)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer > damageRate)
            {
                DealDamage();
                damageTimer -= damageRate;
            }
        }
    }
}
