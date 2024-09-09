using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Spell : MonoBehaviour
{
    public SpellCraft.Aspect mainAspect = SpellCraft.Aspect.none;
    public SpellCraft.Aspect modAspect = SpellCraft.Aspect.none;
    public int burstDamage;
    public int DOTDamage;
    public int DOTDuration;
    public GameObject AOEPrefab;
    public int AOEDuration;
    public bool lifeSteal = false;
    public float lifeStealRatio = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burst(GameObject target)
    {
        target.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(burstDamage);
    }

    public void ApplyDOT(GameObject target)
    {
        if(target.GetComponent<DOT>() == null)
        {
            target.AddComponent<DOT>();
        }
    }

    public void SpellEffect(GameObject target)
    {
        if (mainAspect == SpellCraft.Aspect.splendor)
        {
            GameObject AOE = Instantiate(AOEPrefab, gameObject.transform.position, transform.rotation);
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
            GameObject.FindWithTag("Player").GetComponent<SuperPupSystems.Helper.Health>().Heal((int)(change*lifeStealRatio));
        }
    }
}
