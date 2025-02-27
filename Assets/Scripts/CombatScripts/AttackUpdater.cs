using UnityEngine;

public class AttackUpdater : MonoBehaviour
{
    //elements
    //Direction??
    public GameObject player = null;
    public SpellCraft.Aspect element = SpellCraft.Aspect.none;
    public int aspect = 0;
    public int spellCost = 5;
    public Spell[] spells;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spells = gameObject.GetComponentsInChildren<Spell>();

        foreach(Spell spell in spells)
        {
            spell.CurrentElement = element;
            spell.subAspect = aspect;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendEnergy(int CostMod)
    {
        player.GetComponent<SpellCraft>().SpendEnergy(element, CostMod);
    }
}
