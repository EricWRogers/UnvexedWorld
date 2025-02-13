using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHUD : MonoBehaviour
{
    // HUD Images for power system and weapon indicators
    public Image[] powerSystemImages; // Array of three images for the power system

    public GameObject spellReady;

    public SpellCraft spellCraft;

    public List<Color> colors = new List<Color>();

    // Colors for key indicators
    
    private Color defaultColor = Color.white; // Default color for empty slots
    private Color highlightColor = Color.yellow; // Color for highlighting selected weapons
    private Color green = Color.green; // Color for combining spells
    private Color purple = new Color(0.5f, 0, 0.5f); // Purple for reset

    void Start()
    {
        colors.Add(Color.clear);
        colors.Add(Color.blue);
        colors.Add(Color.red);
        colors.Add(Color.yellow);
        spellReady.SetActive(false);
        spellCraft = GameObject.FindWithTag("Player").GetComponentInParent<SpellCraft>();
    }

    // Update is called once per frame
    void Update()
    {
        powerSystemImages[0].color = colors[(int)spellCraft.CurrentElement];


        // Check if F key is held down for combining spells
        
        spellReady.SetActive(spellCraft.casting);
    }
}
