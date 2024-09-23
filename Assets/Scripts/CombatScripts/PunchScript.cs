using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PunchScript : MonoBehaviour
{
    
    public int damage = 1;

    public GameObject enemy;

    public UnityEvent<GameObject> punchTarget;
    public GameObject particle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
      
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit" + other.gameObject.name);
        if (other.gameObject.tag == "GroundEnemy" || other.gameObject.tag == "Enemy")
        {   
            enemy = other.gameObject;
            if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
            {
                enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.AddListener(gameObject.GetComponent<Spell>().LifeSteal);
            }
            other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            punchTarget.Invoke(enemy);
            Debug.Log(" Enemy Hit");
            if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
            {
                enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.RemoveListener(gameObject.GetComponent<Spell>().LifeSteal);
            }
        }
        

    }
    public void Punch(GameObject enemy)
    {
        
           
    }

    public void StartParticle()
    {
        Debug.Log("Null???");
        if(gameObject.GetComponent<Spell>() != null)
        {
            Debug.Log("StartParticle notNull");
            if(gameObject.GetComponent<Spell>().mainAspect == SpellCraft.Aspect.scavenge)
            {
                particle = Instantiate(ParticleManager.Instance.ScavengeParticle, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
            else if(gameObject.GetComponent<Spell>().mainAspect == SpellCraft.Aspect.splendor)
            {
                particle = Instantiate(ParticleManager.Instance.SplendorParticle, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
        }
        else
        {
            Debug.Log("Null");
            //default particle
            particle = null;
        }
    }

    public void EndParticle()
    {
        if (particle != null)
        {
            Destroy(particle);
        }
    }
}
