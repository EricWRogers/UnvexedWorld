using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SuperPupSystems.Helper;

public class Ricochet : MonoBehaviour
{
    public int damage = 5;
    public float speed = 5;
    public float range = 20;
    public int ricochetCount = 0;
    public List<GameObject> hitEnemies;
    public GameObject target;
    public TargetingSystem targetingSystem;

    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    void Start()
    {
        targetingSystem = gameObject.AddComponent<TargetingSystem>();
        targetingSystem.targetTag = "GroundEnemy";
        target = targetingSystem.TargetExcluding(hitEnemies,range);
        if(target != null)
        {
            gameObject.transform.LookAt(target.transform);
        }
    }
    

    public void Target()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if ((other.gameObject.CompareTag("GroundEnemy") || other.gameObject.CompareTag("Enemy")) && other.GetComponent<Rigidbody>().isKinematic == true)
        {
            if (other.gameObject.GetComponent<Health>() == null || hitEnemies.Contains(other.gameObject))
            {
                return;
            }
            Instantiate(ParticleManager.Instance.SunderImpact, transform.position, Quaternion.Euler(transform.rotation.x,transform.rotation.y,transform.rotation.z));
            other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            RicochetBounce(other);
            Destroy(gameObject);
        }
    }

    public void RicochetBounce(Collider other)
    {
        if(ricochetCount==0)
        {
            return;
        }
        GameObject tempRicochet = Instantiate(AttackManager.Instance.sunderCrystalPrefabs[1],other.transform.position + new Vector3(0,2,0),transform.rotation);
        tempRicochet.GetComponent<Ricochet>().ricochetCount = ricochetCount-1;
        tempRicochet.GetComponent<Ricochet>().hitEnemies = hitEnemies;
        tempRicochet.GetComponent<Ricochet>().hitEnemies.Insert(hitEnemies.Count,other.gameObject);
    }

}
