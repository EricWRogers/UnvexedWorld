using UnityEngine;
using SuperPupSystems.Helper;

public class KillEnemyPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("GroundEnemy"))
        {
            other.GetComponent<Health>().Kill();
        }
    }
}
