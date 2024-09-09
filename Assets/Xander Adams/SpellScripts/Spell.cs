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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burst()
    {

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
        if (modAspect == SpellCraft.Aspect.scavenge)
        {
            ApplyDOT(target);
        }
    }
}
