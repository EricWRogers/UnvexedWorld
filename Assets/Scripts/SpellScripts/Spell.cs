using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;

public class Spell : MonoBehaviour, IDamageDealer
{
    public SpellCraft.Aspect mainAspect = SpellCraft.Aspect.none;
    public SpellCraft.Aspect modAspect = SpellCraft.Aspect.none;
    public int burstDamage;
    //public GameObject AOEPrefab;
    //public GameObject DOTParticle;
    public int AOEDuration;
    public bool lifeSteal = false;
    public float lifeStealRatio = 1f;

    public ComboManager comboManager;
    
    // Start is called before the first frame update
    void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burst(GameObject target)
    {
        target.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(burstDamage);
        MessageSpawner messageSpawner = target.GetComponentInChildren<MessageSpawner>();
        if (messageSpawner != null)
        {
            messageSpawner.ApplyDamage(gameObject); // Pass the gameObject that dealt the damage
        }
        Instantiate(ParticleManager.Instance.BurstParticle, target.transform.position, target.transform.rotation);

        if (comboManager != null)
        {
            comboManager.IncrementCombo();
        }
    }

    public void ApplyDOT(GameObject target)
    {
        if(target.GetComponent<DOT>() == null)
        {
            target.AddComponent<DOT>();
            target.GetComponent<DOT>().particle = ParticleManager.Instance.DOTParticle;
        }
    }

    public void SpellEffect(GameObject target)
    {
        if (mainAspect == SpellCraft.Aspect.splendor)
        {
            GameObject AOE = Instantiate(ParticleManager.Instance.AOE, gameObject.transform.position, transform.rotation);
            AOE.GetComponent<Spell>().modAspect = modAspect;
        }
        else
        {
            // if (mainAspect == SpellCraft.Aspect.scavenge)
            // {
            //     lifeSteal = true;
            // }
            if (modAspect == SpellCraft.Aspect.scavenge)
            {
                ApplyDOT(target);
            }
            if (modAspect == SpellCraft.Aspect.splendor)
            {
                Burst(target);
            }
        }
        //lifeSteal = false;
        
    }

    public void LifeSteal(HealthChangedObject healthChangedObject)
    {
        int change = healthChangedObject.delta*-1;
        Debug.Log("Heal" + change*lifeStealRatio);
        if(change>0)
        {
            //GameObject.FindWithTag("Player").GetComponent<SuperPupSystems.Helper.Health>().Heal((int)(change*lifeStealRatio));
            for (int i = 0; i < change; i++)
            {
                Instantiate(ParticleManager.Instance.LifeStealOrb, gameObject.transform.position, transform.rotation);   
            }
        }
    }

    public int GetDamage()
    {
        return burstDamage;
    }

    public void SetSelf(SpellCraft.Aspect newMainAspect,SpellCraft.Aspect newModAspect)
    {
        SetMain(newMainAspect);
        SetMod(newModAspect);
    }

    public void SetMain(SpellCraft.Aspect aspect)
    {
        mainAspect = aspect;
        if(aspect == SpellCraft.Aspect.scavenge)
        {
            lifeSteal=true;
        }
        else
        {
            lifeSteal=false;
        }
    }

    public void SetMod(SpellCraft.Aspect aspect)
    {
        modAspect = aspect;
    }

    public void ClearSpell()
    {
        SetMain(SpellCraft.Aspect.none);
        SetMod(SpellCraft.Aspect.none);
    }

    
}
