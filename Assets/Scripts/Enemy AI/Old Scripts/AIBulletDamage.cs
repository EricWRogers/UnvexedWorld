using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBulletDamage : MonoBehaviour
{
    public int damage = 5;

    public GameObject impactPS;

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
            impactPS = Instantiate(ParticleManager.Instance.EnemyRangeImpact, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
            col.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            Destroy(gameObject, .05f);
            Destroy(impactPS, 1.0f);
        }

        if (col.gameObject.CompareTag("GroundEnemy") == false)
            Destroy(gameObject, .05f);

        Debug.Log("Hit: " + col.gameObject.name + "");
    }
}
