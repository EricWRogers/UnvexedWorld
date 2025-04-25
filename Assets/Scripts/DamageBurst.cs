using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SuperPupSystems.Helper;

public class DamageBurst : MonoBehaviour
{
    public float damageSeconds = 10f;
    public float damageInterval = 2f;

    private bool playerInZone = false;
    private float timer = 0f;
    private Health playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerInZone = true;
                timer = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            playerHealth = null;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                playerHealth.Damage((int)damageSeconds);
                timer = 0f;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
}
