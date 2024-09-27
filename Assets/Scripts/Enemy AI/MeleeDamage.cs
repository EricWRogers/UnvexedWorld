    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class MeleeDamage : MonoBehaviour
{
    public int dmg;
    public Health playerHealth;

    public AttackState attackState;

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
            col.gameObject.GetComponent<Health>().Damage(dmg);
        }
    }

    public void DealDamage()
    {
        Debug.Log("Enemy attacking");
        playerHealth.Damage(dmg);
    }
}
