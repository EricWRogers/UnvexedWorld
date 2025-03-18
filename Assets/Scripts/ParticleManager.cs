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

    public GameObject EnemySlashParticle;

    private GameObject stunPS;
    private GameObject EnSlash;
    
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
        stunPS = Instantiate(StunParticle, spawnLocation.position, spawnLocation.rotation);
        stunPS.transform.parent=stuned.transform;
    }
    public void DestroyStunParticles()
    {
        Destroy(stunPS);
    }

    public void SpawnEnemySlash(Transform spawnLocation)
    {
        EnSlash = Instantiate(EnemySlashParticle, spawnLocation.position, spawnLocation.rotation);
        EnSlash.transform.parent = spawnLocation;
    }
    


}
