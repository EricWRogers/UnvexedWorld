using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class MeleeDamage : MonoBehaviour
{
    public int dmg;
    private float damageCooldown = 0.5f;
    private float lastDamageTime;
    public bool didDamage;
    public Health playerHealth;
    public ParticleSystem hit1;
    public ParticleSystem hit2;
    public bool isBoss;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.Find("Player").GetComponent<Health>();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown)
        {
            if(hit1 != null && hit2 != null)
            {
                hit1.Play();
                hit2.Play();
            }

            // Calculate the hit direction
            Vector3 hitDir = col.gameObject.transform.position - gameObject.transform.position;

            // Apply damage to the player
            col.gameObject.GetComponent<Health>().Damage(dmg);
            lastDamageTime = Time.deltaTime;

            // Apply knockback to the player
            PlayerKnockback playerKnockback = col.gameObject.GetComponent<PlayerKnockback>();
            if (playerKnockback != null)
            {
                playerKnockback.ApplyKnockback(hitDir);
            }
            if(!isBoss)
            {
                Destroy(gameObject, damageCooldown + 0.25f);
            }
        }
    }
}
