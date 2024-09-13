using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject spellPrefab;
    public GameObject camera;
    public float bulletSpeed = 10f;

    public float fireRate = 0.2f;
    public float weaponRange = 50f;
    public int damagePerShot = 20;

    private float nextFireTime;


    private void Start()
    {

    }

    private void Awake()
    {
        camera = GameObject.Find("MainCamera");
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void ShootPrefab()
    {
        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, camera.transform.rotation);
        //Rigidbody rigi = bullet.GetComponentInChildren<Rigidbody>();
        //rigi.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }

    public void ShootSpellPrefab(SpellCraft.Aspect mainAspect, SpellCraft.Aspect modAspect)
    {
        nextFireTime = Time.time + fireRate;
        GameObject bullet = Instantiate(spellPrefab, firePoint.position, (camera.transform.rotation+transform.rotation)/2);
        if (mainAspect == SpellCraft.Aspect.scavenge)
            {
                bullet.GetComponent<Spell>().lifeSteal = true;
            }
            else
            {
                bullet.GetComponent<Spell>().lifeSteal = false;
            }
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
            if (hit.transform.CompareTag("GroundEnemy"))
            {
                Debug.Log("Hit!");
            }
        }
    }
}
