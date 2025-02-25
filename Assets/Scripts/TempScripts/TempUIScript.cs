using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TempUIScript : MonoBehaviour
{

    public TMP_Text elmText;

    public SpellCraft cE;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elmText.text = "Current Element: " + cE.CurrentElement;
    }
}
