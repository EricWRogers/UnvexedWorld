using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scripts.HUDScripts.MessageSystem;



public class PunchScript : MonoBehaviour, IDamageDealer
{
    
    public int damage = 1;

    public GameObject enemy;

    public UnityEvent<GameObject> punchTarget;
    public GameObject particle;

    public ComboManager comboManager;
    private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
         
        comboManager = FindObjectOfType<ComboManager>();
        audioManager = FindObjectOfType<AudioManager>();
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
            PlayPunch();

            Instantiate(ParticleManager.Instance.NoSpellImpact, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
            //gameObject.GetComponentInParent<SpellCraft>().RegenMana(10);

            enemy = other.gameObject;
            if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
            {
                enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.AddListener(gameObject.GetComponent<Spell>().LifeSteal);
            }
            
            other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            MessageSpawner messageSpawner = enemy.GetComponentInChildren<MessageSpawner>();
            if (messageSpawner != null)
            {
                messageSpawner.ApplyDamage(gameObject); // Pass the gameObject that dealt the damage
            }
            other.GetComponent<Knockback>().OnHurt();
            punchTarget.Invoke(enemy);
            Debug.Log(" Enemy Hit");

            // Increment the combo count
            if (comboManager != null)
            {
                comboManager.IncrementCombo();
            }
            
            if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
            {
                enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.RemoveListener(gameObject.GetComponent<Spell>().LifeSteal);
            }
        }
        

    }
   

    public void StartParticle()
    {
        if (particle!= null)
        {
            EndParticle();
        }
        if(gameObject.GetComponent<Spell>() != null)
        {
            if(gameObject.GetComponent<Spell>().mainAspect == SpellCraft.Aspect.scavenge || (gameObject.GetComponent<Spell>().mainAspect == SpellCraft.Aspect.none && gameObject.GetComponent<Spell>().modAspect == SpellCraft.Aspect.scavenge))
            {
                particle = Instantiate(ParticleManager.Instance.ScavengeParticleMelee, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
            else if(gameObject.GetComponent<Spell>().mainAspect == SpellCraft.Aspect.splendor|| (gameObject.GetComponent<Spell>().mainAspect == SpellCraft.Aspect.none && gameObject.GetComponent<Spell>().modAspect == SpellCraft.Aspect.splendor))
            {
                particle = Instantiate(ParticleManager.Instance.SplendorParticleMelee, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
        }
        else
        {
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

    public int GetDamage()
    {
        return damage;
    }

    public void PlayPunch()
    {
        if (audioManager != null)
        {
            FindObjectOfType<AudioManager>().PlayPunchSound();
        }
        else
        {
            Debug.Log("AudioManager not found! Punch no play");
        }
    }
}
