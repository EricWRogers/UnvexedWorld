using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangedAttack : MonoBehaviour
{
    public SpellCraft spellCraft;
    public SpellShot spellShot;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Q))
        {
            // if (spellCraft.casting && spellCraft.mainAspect!=SpellCraft.Aspect.none)
            // {
            //     spellCraft.CastSpell(SpellCraft.CastType.melee, spellCraft.mainAspect, spellCraft.modAspect);
            // }
            Melee();
        }
        else if (Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.E))
        {
            // if (spellCraft.casting && spellCraft.mainAspect!=SpellCraft.Aspect.none)
            // {
            //     spellCraft.CastSpell(SpellCraft.CastType.ranged, spellCraft.mainAspect, spellCraft.modAspect);
            // }
            
            Range();
        }
    }

    private void Melee()
    {
        GetComponent<Animator>().SetTrigger("Melee");
    }

    private void Range()
    {
        GetComponent<Animator>().SetTrigger("Ranged");
        //spellShot.ShootPrefab();
    }

    private void Damage()
    {

    }
}
