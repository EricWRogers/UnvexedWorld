using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject spellPrefab;
    public GameObject targetCamera;
    public Vector3 aimRotation;
    private SpellCraft spellCraft;
    public float bulletSpeed = 10f;

    public float fireRate = 0.2f;
    public float weaponRange = 50f;
    public int damagePerShot = 20;
    public int aimOffset = 8;

    private float nextFireTime;


    private void Start()
    {

    }

    void Awake() 
    {
        targetCamera = GameObject.FindWithTag("MainCamera");
        spellCraft = GetComponent<SpellCraft>();
    }
    // Update is called once per frame
    void Update()
    {
        //aimRotation = targetCamera.transform.rotation;
    }

    public void ShootPrefab()
    {
        aimRotation = targetCamera.transform.rotation.eulerAngles;
        aimRotation.x -= aimOffset;
        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(aimRotation));
        Vector3 dir = (transform.position - targetCamera.transform.position).normalized;
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
        //Rigidbody rigi = bullet.GetComponentInChildren<Rigidbody>();
        //rigi.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 3f);
    }

    public void ShootSpellPrefab()
    {
        if (spellCraft.CurrentElement != SpellCraft.Aspect.none)
        {
            ShootSpellPrefab(spellCraft.CurrentElement);
        }
        else
        {
            ShootPrefab();
        }
    }

    public void ShootSpellPrefab(SpellCraft.Aspect CurrentElement)
    {
        aimRotation = targetCamera.transform.rotation.eulerAngles;
        aimRotation.x -= aimOffset;
        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(spellPrefab, firePoint.position, Quaternion.Euler(aimRotation));
        Vector3 dir = (transform.position - targetCamera.transform.position).normalized;
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
        if (CurrentElement == SpellCraft.Aspect.scavenge)
            {
                bullet.GetComponent<Spell>().lifeSteal = true;
            }
            else
            {
                bullet.GetComponent<Spell>().lifeSteal = false;
            }
        bullet.GetComponent<Spell>().CurrentElement = CurrentElement;
        
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
