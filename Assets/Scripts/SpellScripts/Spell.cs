using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;
using Unity.Mathematics;

public class Spell : MonoBehaviour, IDamageDealer
{
    public SpellCraft.Aspect CurrentElement = SpellCraft.Aspect.none;
    
    public int subAspect = 0;
    public int burstDamage = 15;
    public float crystalDamage = 20;
    //public GameObject AOEPrefab;
    //public GameObject DOTParticle;
    public int AOEDuration;
    public bool lifeSteal = false;
    public bool overwriteSpell = false;
    public float lifeStealRatio = 1f;
    public int ricochetCount = 2;

    public ComboManager comboManager;
    
    // Start is called before the first frame update
    void Start()
    {
        comboManager = FindFirstObjectByType<ComboManager>();
        if(gameObject.GetComponent<PunchScript>()!=null)
        {
            gameObject.GetComponent<PunchScript>().punchTarget.AddListener(SpellEffect);
        }
        if(gameObject.GetComponentInParent<AttackUpdater>()!=null && overwriteSpell == false)
        {
            AttackUpdater temp = gameObject.GetComponentInParent<AttackUpdater>();
            CurrentElement = temp.element;
            subAspect = temp.aspect;
        }
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
    public void ApplyCrystalize(GameObject target)
    {
        
        Instantiate(ParticleManager.Instance.SunderImpact, target.transform.position, transform.rotation);
        if(target.GetComponent<Crystalize>() == null)
        {
            target.AddComponent<Crystalize>();
            target.GetComponent<Crystalize>().particle = ParticleManager.Instance.CrystalizeParticle;
            target.GetComponent<Crystalize>().fullCrystal = ParticleManager.Instance.CrystalizedObject;
        }
        target.GetComponent<Crystalize>().crystalization += crystalDamage;
    }

    public void Ricochet(GameObject target)
    {
        Instantiate(ParticleManager.Instance.SunderImpact, target.transform.position, transform.rotation);
        GameObject tempRicochet = Instantiate(AttackManager.Instance.sunderCrystalPrefabs[1], target.transform.position + new Vector3(0,2,0),transform.rotation);
        tempRicochet.GetComponent<Ricochet>().ricochetCount = 2;
        tempRicochet.GetComponent<Ricochet>().hitEnemies = new List<GameObject>{target};
    }

    public void CrystalTrap()
    {
        Instantiate(AttackManager.Instance.sunderCrystalPrefabs[0],transform.position + new Vector3(0,1,0),quaternion.identity);
    }

    public void SpellEffect(GameObject target)
    {
        if(subAspect == 0)
        {
            if (CurrentElement == SpellCraft.Aspect.splendor)
            {
                GameObject AOE = Instantiate(ParticleManager.Instance.AOE, target.transform.position, transform.rotation);
            }
            if (CurrentElement == SpellCraft.Aspect.scavenge)
            {
                lifeSteal = true;
            }
            if (CurrentElement == SpellCraft.Aspect.sunder)
            {
                Ricochet(target);
            }
        }
        else if (subAspect == 1)
        {
            
            if (CurrentElement == SpellCraft.Aspect.scavenge)
            {
                ApplyDOT(target);
            }
            if (CurrentElement == SpellCraft.Aspect.splendor)
            {
                Burst(target);
            }
            if (CurrentElement == SpellCraft.Aspect.sunder)
            {
                ApplyCrystalize(target);
            }
        }        
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

    public void SetSelf(SpellCraft.Aspect newCurrentElement)
    {
        SetMain(newCurrentElement);
    }

    public void SetMain(SpellCraft.Aspect aspect)
    {
        CurrentElement = aspect;
        if(aspect == SpellCraft.Aspect.scavenge)
        {
            lifeSteal=true;
        }
        else
        {
            lifeSteal=false;
        }
    }

    public void ClearSpell()
    {
        SetMain(SpellCraft.Aspect.none);
    }

    
}
