using UnityEngine;
using UnityEngine.Events;

public class SuperPunch : MonoBehaviour
{
    
    public int[] energy ={0,100,100,100};
    public int spellBonus = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spellBonus = (energy[1]+energy[2]+energy[3])/5;
        SetUpSpells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpSpells()
    {
        UnityEvent<GameObject> hitEvent = null;
        if(gameObject.GetComponent<PunchScript>()!=null)
        {
            hitEvent = gameObject.GetComponent<PunchScript>().punchTarget;
            gameObject.GetComponent<PunchScript>().spellBonus = spellBonus;
        }
        else if(gameObject.GetComponent<ProjectileSpell>()!=null)
        {
            hitEvent = gameObject.GetComponent<ProjectileSpell>().hitTarget;
            gameObject.GetComponent<ProjectileSpell>().spellBonus = spellBonus;
        }
        if(hitEvent!=null)
        {
            if(energy[1]>20)
            {
                Spell tempscavspell = gameObject.AddComponent<Spell>();
                tempscavspell.CurrentElement= SpellCraft.Aspect.scavenge;
                tempscavspell.subAspect= 0;
                hitEvent.AddListener(tempscavspell.SpellEffect);
                Spell tempscavspell2 = gameObject.AddComponent<Spell>();
                tempscavspell2.CurrentElement= SpellCraft.Aspect.scavenge;
                tempscavspell2.subAspect= 1;
                hitEvent.AddListener(tempscavspell2.SpellEffect);
            }   
            if(energy[2]>20)
            {
                Spell tempsplenspell = gameObject.AddComponent<Spell>();
                tempsplenspell.CurrentElement= SpellCraft.Aspect.splendor;
                tempsplenspell.subAspect= 0;
                hitEvent.AddListener(tempsplenspell.SpellEffect);
                Spell tempsplenspell2 = gameObject.AddComponent<Spell>();
                tempsplenspell2.CurrentElement= SpellCraft.Aspect.splendor;
                tempsplenspell2.subAspect= 1;
                hitEvent.AddListener(tempsplenspell2.SpellEffect);
            }
            if(energy[3]>20)
            {
                Spell tempsundspell = gameObject.AddComponent<Spell>();
                tempsundspell.CurrentElement= SpellCraft.Aspect.sunder;
                tempsundspell.subAspect= 0;
                tempsundspell.ricochetCount = energy[3]/10;
                hitEvent.AddListener(tempsundspell.SpellEffect);
                Spell tempsundspell2 = gameObject.AddComponent<Spell>();
                tempsundspell2.CurrentElement= SpellCraft.Aspect.sunder;
                tempsundspell2.subAspect= 1;
                tempsundspell2.crystalDamage=energy[3];
                hitEvent.AddListener(tempsundspell2.SpellEffect);
            }
        }
    }
}
