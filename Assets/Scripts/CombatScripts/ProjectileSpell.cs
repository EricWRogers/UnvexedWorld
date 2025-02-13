using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;



[RequireComponent(typeof(Timer))]
public class ProjectileSpell : MonoBehaviour, IDamageDealer
{
    public int damage = 1;
    public float speed = 20f;
    public float lifeTime = 10f;
    public bool destroyOnImpact = true;
    public UnityEvent<GameObject> hitTarget;
    public LayerMask mask;
    public List<string> tags;
    public GameObject particle;
    private Vector3 m_lastPosition;
    private RaycastHit m_info;
    private Timer m_timer;
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
        
        StartParticle();
    }
    private void FixedUpdate()
    {
        Move();
        CollisionCheck();
        m_lastPosition = transform.position;
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
                if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
                {
                    m_info.transform.gameObject.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.AddListener(gameObject.GetComponent<Spell>().LifeSteal);
                }
                m_info.transform.GetComponent<Health>()?.Damage(damage);
                m_info.transform.GetComponent<Knockback>()?.OnHurt();
                MessageSpawner messageSpawner = m_info.transform.GetComponentInChildren<MessageSpawner>();
                if (messageSpawner != null)
                {
                    messageSpawner.ApplyDamage(gameObject); // Pass the gameObject that dealt the damage
                }
                hitTarget.Invoke(m_info.transform.gameObject);
                if(gameObject.GetComponent<Spell>()?.lifeSteal == true)
                {
                    m_info.transform.gameObject.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.RemoveListener(gameObject.GetComponent<Spell>().LifeSteal);
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
}
