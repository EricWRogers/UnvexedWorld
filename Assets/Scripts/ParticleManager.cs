using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; } = null;

    public GameObject AOE;
    public GameObject DOTParticle;
    public GameObject LifeStealOrb;
    public GameObject BurstParticle;
    public GameObject ScavengeParticle;
    public GameObject SplendorParticle;

    public GameObject ScavengeParticleMelee;
    public GameObject SplendorParticleMelee;
    public GameObject NoSpellImpact;
    public GameObject StunParticle;

    public GameObject EnemySlash;

    private GameObject stunPS;
    
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnStunParticles(Transform spawnLocation, GameObject stuned)
    {
        stunPS = GameObject.Instantiate(StunParticle, spawnLocation.position, spawnLocation.rotation);
        stunPS.transform.parent=stuned.transform;
    }
    public void DestroyStunParticles()
    {
        Destroy(stunPS);
    }

    public void spawnEnemySlash(Transform spawnLocation)
    {
        EnemySlash = Instantiate(ParticleManager.Instance.EnemySlash, transform.position, Quaternion.Euler(transform.rotation.x-90,transform.rotation.y,transform.rotation.z));
        EnemySlash.transform.parent = gameObject.transform;
    }

}
