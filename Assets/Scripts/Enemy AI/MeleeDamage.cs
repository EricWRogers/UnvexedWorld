using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class MeleeDamage : MonoBehaviour
{
    public int dmg;
    public Health playerHealth;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.Find("Player").GetComponent<Health>();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            // Calculate the hit direction
            Vector3 hitDir = col.gameObject.transform.position - gameObject.transform.position;

            // Apply damage to the player
            col.gameObject.GetComponent<Health>().Damage(dmg);

            // Apply knockback to the player
            PlayerKnockback playerKnockback = col.gameObject.GetComponent<PlayerKnockback>();
            if (playerKnockback != null)
            {
                playerKnockback.ApplyKnockback(hitDir);
            }
        }
    }
}
