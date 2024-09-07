using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject spellPrefab;
    public float bulletSpeed = 10f;

    public float fireRate = 0.2f;
    public float weaponRange = 50f;
    public int damagePerShot = 20;

    private float nextFireTime;


    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void ShootPrefab()
    {
        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        //Rigidbody rigi = bullet.GetComponentInChildren<Rigidbody>();
        //rigi.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }

    public void ShootSpellPrefab(SpellCraft.Aspect mainAspect, SpellCraft.Aspect modAspect)
    {
        nextFireTime = Time.time + fireRate;
        GameObject bullet = Instantiate(spellPrefab, firePoint.position, transform.rotation);
        bullet.GetComponent<Spell>().mainAspect = mainAspect;
        bullet.GetComponent<Spell>().modAspect = modAspect;
        
        //Rigidbody rigi = bullet.GetComponentInChildren<Rigidbody>();
        //rigi.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }


    void ShootRaycast()
    {
        nextFireTime = Time.time + fireRate;

        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, weaponRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit!");
            }
        }
    }
}
