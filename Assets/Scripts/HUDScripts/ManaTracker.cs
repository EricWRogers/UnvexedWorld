using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaTracker : MonoBehaviour
{
    public TMP_Text splendorMana;
    public TMP_Text scavengeMana;
    public SpellCraft spellCraft;
    // Start is called before the first frame update
    void Start()
    {
        spellCraft = GameObject.FindWithTag("Player").GetComponent<SpellCraft>();
    }

    // Update is called once per frame
    void Update()
    {
        splendorMana.text = ("%"+(int)spellCraft.splendorMana);
        scavengeMana.text = ("%"+(int)spellCraft.scavengeMana);
    }
}
