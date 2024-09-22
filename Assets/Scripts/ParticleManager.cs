using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; } = null;

    public GameObject AOE;
    public GameObject DOTParticle;
    public GameObject LifeStealParticle;
    public GameObject BurstParticle;
    public GameObject ScavnegeParticle;
    public GameObject SplendorParticle;
    
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
}
