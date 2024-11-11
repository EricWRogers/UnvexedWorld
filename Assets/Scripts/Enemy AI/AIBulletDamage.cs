using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBulletDamage : MonoBehaviour
{
    public int damage = 5;

    public LayerMask ignoreLayer;

    public void OnTriggerEnter(Collider col)
    {
        //Ignore Certain Layer (Corruption)
        if (((1 << col.gameObject.layer) & ignoreLayer) != 0)
        {
            return;
        }

        if(col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            Destroy(gameObject, .05f);
        }

        if (col.gameObject.CompareTag("GroundEnemy") == false)
            Destroy(gameObject, .05f);

        Debug.Log("Hit: " + col.gameObject.name + "");
    }
}
