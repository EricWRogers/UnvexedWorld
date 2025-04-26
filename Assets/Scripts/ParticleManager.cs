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
    public GameObject SunderParticle;
    
    public GameObject SunderImpact;
    
    public GameObject CrystalizeParticle;
    public GameObject CrystalizedObject;

    public GameObject ScavengeParticleMelee;
    public GameObject SplendorParticleMelee;
    public GameObject NoSpellImpact;
    public GameObject StunParticle;

    public GameObject EnemySlashParticle;

    public GameObject RangedHitEffect;
    public GameObject EnemyRangeImpact;

    private GameObject stunPS;
    private GameObject EnSlash;

    public GameObject BossStomp;
    public GameObject BossSlam;
    public GameObject BossCharge;
    public GameObject BossRoar;
    
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

    public void SpawnBossStomp(Transform spawnLocation)
    {
        EnSlash = Instantiate(BossStomp, spawnLocation.position, spawnLocation.rotation);
        EnSlash.transform.parent = spawnLocation;
    }

    public void SpawnBossSlam(Transform spawnLocation)
    {
        EnSlash = Instantiate(BossSlam, spawnLocation.position, spawnLocation.rotation);
        EnSlash.transform.parent = spawnLocation;
    }

    public void SpawnBossRoar(Transform spawnLocation)
    {
        EnSlash = Instantiate(BossRoar, spawnLocation.position, spawnLocation.rotation);
        EnSlash.transform.parent = spawnLocation;
    }

    public void SpawnBossCharge(Transform spawnLocation)
    {
        EnSlash = Instantiate(BossCharge, spawnLocation.position, spawnLocation.rotation);
        EnSlash.transform.parent = spawnLocation;
    }

    public void DestroyBossCharge()
    {
        Destroy(EnSlash);
    }
    


}
