using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SuperPupSystems.Helper;

public class ManaTracker : MonoBehaviour
{
  

    public List<Sprite> elementSprites;
    public List<Color> elementColors;

    public GameObject box;

    public SpellCraft spellCraft;

    public Slider manaSlider;
    public Image fillImage; 
    // Start is called before the first frame update
    void Start()
    {
        spellCraft = GameObject.FindWithTag("Player").GetComponent<SpellCraft>();
    }

    // Update is called once per frame
    void Update()
    {
        int elementIndex = (int)spellCraft.CurrentElement;
        float currentMana = spellCraft.energy[elementIndex];
        float manaPercent = currentMana / 100f;

        manaSlider.value = manaPercent;
        fillImage.color = elementColors[elementIndex];

        
        box.GetComponent<Image>().sprite = elementSprites[(int)spellCraft.CurrentElement];
        
    }
}
