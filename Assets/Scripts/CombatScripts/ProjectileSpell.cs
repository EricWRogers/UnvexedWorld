using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;



[RequireComponent(typeof(Timer))]
public class ProjectileSpell : MonoBehaviour, IDamageDealer
{
    [Tooltip("Please type between 1-3 for the type of Knock Back you want (1 : Push, 2 : AOE, 3 : Closer)")]
    public int knockBackType;
    public bool going = true;
    public int damage = 1;
    public float speed = 20f;
    public float lifeTime = 10f;
    public bool destroyOnImpact = true;
    public UnityEvent<GameObject> hitTarget;
    public UnityEvent activate;
    public LayerMask mask;
    public List<string> tags;
    public GameObject particle;
    private Vector3 m_lastPosition;
    private RaycastHit m_info;
    private Timer m_timer;
    public GameObject enemy;
    
    public Transform direction;
    
    public float forceAmount = 4f;
    private void Awake()
    {
        if (hitTarget == null)
        {
            hitTarget = new UnityEvent<GameObject>();
        }
    }
    private void Start()
    {
        m_timer = GetComponent<Timer>();
        m_timer.timeout.AddListener(DestroyBullet);
        m_timer.StartTimer(lifeTime);
        // set init position
        m_lastPosition = transform.position;
        if(going)
        {
            Target();
        }
        
        StartParticle();
    }
    private void FixedUpdate()
    {
        if(going)
        {
            Move();
            CollisionCheck();
            m_lastPosition = transform.position;
        }
    }
    private void Move()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
    private void CollisionCheck()
    {
        if (Physics.Linecast(m_lastPosition, transform.position, out m_info, mask))
        {
            if (tags.Contains(m_info.transform.tag))
            {
                 enemy = m_info.transform.gameObject;
                hitTarget.Invoke(enemy);
                if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
                {
                    enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.AddListener(gameObject.GetComponent<Spell>().LifeSteal);
                }
                m_info.transform.GetComponent<Health>()?.Damage(damage);
                m_info.transform.GetComponent<Knockback>()?.OnHurt();
                MessageSpawner messageSpawner = m_info.transform.GetComponentInChildren<MessageSpawner>();
                if (messageSpawner != null)
                {
                    messageSpawner.ApplyDamage(gameObject); // Pass the gameObject that dealt the damage
                }
                if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
                {
                    enemy.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.RemoveListener(gameObject.GetComponent<Spell>().LifeSteal);
                }
                
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
                            direction.LookAt(enemy.transform);
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
            }
            if (destroyOnImpact)
            {
                if(gameObject.GetComponent<Spell>() !=null)
                {
                    if(gameObject.GetComponent<Spell>().CurrentElement == SpellCraft.Aspect.splendor)
                    {
                        gameObject.GetComponent<Spell>().SpellEffect(gameObject);
                    }
                }
                DestroyBullet();
            }
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    public void StartParticle()
    {
        if(gameObject.GetComponent<Spell>() != null)
        {
            if(gameObject.GetComponent<Spell>().CurrentElement == SpellCraft.Aspect.scavenge)
            {
                particle = Instantiate(ParticleManager.Instance.ScavengeParticle, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
            else if(gameObject.GetComponent<Spell>().CurrentElement == SpellCraft.Aspect.splendor)
            {
                particle = Instantiate(ParticleManager.Instance.SplendorParticle, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
                particle.transform.parent = gameObject.transform;
            }
        }
        else
        {
            //default particle
            particle = null;
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Launch()
    {
        Target();
        going = true;
    }
    
    public void Target()
    {
        Debug.Log("target");
        if (gameObject.GetComponentInParent<AttackUpdater>()?.player.GetComponent<MeleeRangedAttack>() != null)
        {
            MeleeRangedAttack temp =  gameObject.GetComponentInParent<AttackUpdater>().player.GetComponent<MeleeRangedAttack>();
            Debug.Log(temp.gameObject.name);
            if (temp.direction)
            {
                gameObject.transform.LookAt(temp.target.transform);
            }
            else
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0,100,0));

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.rigidbody != null)
                    {
                        gameObject.transform.LookAt(hit.point);
                    }
                    else
                    {
                        gameObject.transform.LookAt(hit.point);
                    }
                }
            }
            
        }
        else if (gameObject.GetComponentInParent<AttackUpdater>() == null)
        {
            Debug.Log("Attack Updater is null");
        }
    }
}
