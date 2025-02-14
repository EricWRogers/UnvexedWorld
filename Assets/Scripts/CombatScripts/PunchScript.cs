using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scripts.HUDScripts.MessageSystem;



public class PunchScript : MonoBehaviour, IDamageDealer
{
    [Tooltip("Please type between 1-3 for the type of Knock Back you want (1 : Push, 2 : AOE, 3 : Closer)")]
    public int knockBackType;
    public int damage = 1;
    public float impactValue = 25f;
    public bool doKnockBack;

    public GameObject enemy;

    public UnityEvent<GameObject> punchTarget;
    public GameObject particle;

    public ComboManager comboManager;
    private AudioManager audioManager;

    public HitStop hitStop;

    public float duration = 0.0f;

    public int punchSoundIndex = 0;
    
    //Temp 
    public Transform direction;
    public float forceAmount = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
         
        comboManager = FindFirstObjectByType<ComboManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.GetComponent<Rigidbody>().isKinematic == true && other.gameObject.tag == "GroundEnemy" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit: " + other.gameObject.name + " duration " + duration);
            PlayPunch();
            hitStop.Stop(duration);

            Instantiate(ParticleManager.Instance.NoSpellImpact, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
            //gameObject.GetComponentInParent<SpellCraft>().RegenMana(10);

            enemy = other.gameObject;
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();

            if (enemy.GetComponent<MeleeStateMachine>() != null)
            {
                var enemyGrunt = enemy.GetComponent<MeleeStateMachine>();
                
                switch (knockBackType)
                {
                    case 1:
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 2:
                        //enemyGrunt.TypeTwoKnockBack(direction, forceAmount);
                        direction.LookAt(other.transform);
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 3:
                        enemyGrunt.TypeThreeKnockBack(direction, forceAmount);
                        break;
                    default:
                        Debug.Log("Incorrect Knock back type please use 1-3.");
                        break;
                }
                
            }
            if (enemy.GetComponent<GruntStateMachine>() != null)
            {
                var enemyGrunt = enemy.GetComponent<GruntStateMachine>();
                
                switch (knockBackType)
                {
                    case 1:
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 2:
                        //enemyGrunt.TypeTwoKnockBack(direction, forceAmount);
                        direction.LookAt(other.transform);
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 3:
                        enemyGrunt.TypeThreeKnockBack(direction, forceAmount);
                        break;
                    default:
                        Debug.Log("Incorrect Knock back type please use 1-3.");
                        break;
                }
                
            }

            punchTarget.Invoke(enemy);
            
           
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
            //other.GetComponent<Knockback>().OnHurt();
            Debug.Log(" Enemy Hit");

            // Increment the combo count
            if (comboManager != null)
            {
                comboManager.IncrementCombo();
            }
            
            if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
            {
                gameObject.GetComponent<Spell>().lifeSteal=false;
                enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.RemoveListener(gameObject.GetComponent<Spell>().LifeSteal);
            }
        }
        if(other.gameObject.CompareTag("Breakable"))
        {
            other.GetComponent<Rigidbody>().Sleep();
            other.GetComponent<BreakableObject>().unBrokenObject.SetActive(false);
            other.GetComponent<BreakableObject>().brokenObject.SetActive(true);
            
            if(other.GetComponent<BreakableObject>().rb != null)
            {
                foreach(Rigidbody rigidbodies in other.GetComponent<BreakableObject>().rb)
                {
                    float mag = rigidbodies.linearVelocity.magnitude;
                    Vector3 dir = (transform.position - other.GetComponent<BreakableObject>().unBrokenObject.transform.position).normalized;
                    rigidbodies.AddForce(dir * (other.GetComponent<BreakableObject>().breakPower + mag), ForceMode.Impulse);
                }
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
            if(gameObject.GetComponent<Spell>().CurrentElement == SpellCraft.Aspect.scavenge)
            {
                particle = Instantiate(ParticleManager.Instance.ScavengeParticleMelee, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
            else if(gameObject.GetComponent<Spell>().CurrentElement == SpellCraft.Aspect.splendor)
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
            audioManager.PlayPunchSound();
        }
        else
        {
            Debug.Log("AudioManager not found! Punch no play");
        }
    }
}
