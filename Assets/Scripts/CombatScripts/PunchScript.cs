using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scripts.HUDScripts.MessageSystem;
using SuperPupSystems.Helper;



public class PunchScript : MonoBehaviour, IDamageDealer
{
    [Tooltip("Please type between 1-3 for the type of Knock Back you want (1 : Push, 2 : AOE, 3 : Closer)")]
    public int knockBackType;
    public int damage = 1;
    
    public int spellBonus = 3;
    public int spellCost = 1;
    public float impactValue = 25f;
    public GameObject enemy;

    public UnityEvent<GameObject> punchTarget;
    
    public UnityEvent<int> spendMana;
    public GameObject particle;

    public ComboManager comboManager;
    private AudioManager audioManager;

    public HitStop hitStop;

    public float duration = 0.0f;

    public int punchSoundIndex = 0;
    
    //Aiden's Additions for Knockback 
    public Transform direction;
    public float forceAmount = 4f;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
         
        comboManager = FindFirstObjectByType<ComboManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        punchTarget.AddListener(SpendMana);
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
        if ((other.gameObject.CompareTag("GroundEnemy") || other.gameObject.CompareTag("Enemy")) && other.GetComponent<Rigidbody>().isKinematic == true)
        {
            if (other.gameObject.GetComponent<Health>() == null)
               return;
            
           
            PlayPunch();

            
            hitStop.Stop(duration);

            Instantiate(ParticleManager.Instance.NoSpellImpact, transform.position, transform.rotation);
            //gameObject.GetComponentInParent<SpellCraft>().RegenMana(10);

            enemy = other.gameObject;

            if(hitEnemies.Contains(enemy))
            {
                return;
            }
            hitEnemies.Add(enemy);
            StartCoroutine(RemoveFromList(enemy));

            if (enemy.GetComponent<GruntStateMachine>() != null)
            {
                var enemyGrunt = enemy.GetComponent<GruntStateMachine>();
                switch (knockBackType)
                {
                    case 0:
                        Debug.Log("0 gives no knock back");
                        break;
                    case 1:
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 2:
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
            if (enemy.GetComponent<RangeGruntStateMachine>() != null)
            {
                var enemyGrunt = enemy.GetComponent<RangeGruntStateMachine>();
                switch (knockBackType)
                {
                    case 0:
                        Debug.Log("0 gives no knock back");
                        break;
                    case 1:
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 2:
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
            if (enemy.GetComponent<AgroGruntStateMachine>() != null)
            {
                var enemyGrunt = enemy.GetComponent<AgroGruntStateMachine>();
                switch (knockBackType)
                {
                    case 0:
                        break;
                    case 1:
                        enemyGrunt.TypeOneKnockBack(direction.forward, forceAmount);
                        break;
                    case 2:
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
            if(gameObject.GetComponent<Spell>()?.CurrentElement==SpellCraft.Aspect.none)
                {  
                    other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
                }
                else
                {
                    other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage+spellBonus);
                }
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

    private IEnumerator RemoveFromList(GameObject enemy)
    {
        yield return new WaitForSeconds(5.25f);
        hitEnemies.Remove(enemy);
    }

    public void SpendMana(GameObject ignoreMe)
    {
        spendMana.Invoke(spellCost);
    }
}
