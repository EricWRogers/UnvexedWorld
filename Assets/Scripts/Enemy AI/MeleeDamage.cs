using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class MeleeDamage : MonoBehaviour
{
    public int dmg;
    public Health playerHealth;

    public AttackState attackState;


    public void DealDamage()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.Find("Player").GetComponent<Health>();
        }
        Debug.Log("Enemy attacking");
        playerHealth.Damage(dmg);
    }
}
