using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RadialMenuManager : MonoBehaviour 
{
    public GameObject radialMenu; // Parent object of the menu
    public SpellCraft spellCraft;
    public List<RadialSection> radialSections; // List of radial sections (styles)
    
    private int currentStyleIndex = 2;
    private bool menuActive = false;
    
    private PlayerGamepad gamepad;

    void Awake()
    {
        // Initialize the Gamepad
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Casting.performed += ctx => ToggleMenu(true);
        gamepad.GamePlay.Casting.canceled += ctx => ToggleMenu(false);
        //gamepad.GamePlay.Cycleaspect.performed += ctx => CycleAspect();
       // gamepad.GamePlay.Cycleelement.performed += ctx => CycleElementUp();
        ToggleMenu(false);
    }

    void Start()
    {
        //elementIndex
        spellCraft = FindAnyObjectByType<SpellCraft>();
    }

    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMenu(true);
            HighlightCurrentStyle();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMenu(false);
        }

        if (menuActive && Input.GetKeyDown(KeyCode.E))
        {
            //CycleRadial();
        }
        
        if (menuActive && Input.GetKeyDown(KeyCode.T)) // Cycle Aspect with T
        {
            CycleAspect();
        }

        if (menuActive)
        {
            HighlightCurrentStyle();
        }
    }

    void ToggleMenu(bool state)
    {
        Debug.Log("Toggle Menu: " + state);
        menuActive = state;

        // Show all sections when menu is opened
        if (state)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            HighlightCurrentStyle();
        } 
        else {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void CycleRadial()
    {
        // Cycle through styles
        currentStyleIndex = (currentStyleIndex + 1) % radialSections.Count;
        // TODO: CALL SPELLCRAFT Reset attribute index when changing styles
        HighlightCurrentStyle();
    }

    void HighlightCurrentStyle()
    {
        // Highlight the correct section and apply a hue shift
        for (int i = 0; i < radialSections.Count; i++)
        {
            bool isActive = (i == currentStyleIndex);
            radialSections[i].SetHighlight(true);
            radialSections[i].ShowAttribute((int)spellCraft.CurrentElement-1);

            if (isActive)
            {
                // Apply color shift when selected
                Color newColor = new Color(1.0f, 1.0f, 1.0f); // Shifts hue based on index
                radialSections[i].highlightImage.color = newColor;
            }
            else 
            {
                // Apply color shift when selected
                Color newColor = new Color(0.4f, 0.4f, 0.4f); // Shifts hue based on index
                radialSections[i].highlightImage.color = newColor;
            }
        }
    }


    void CycleAspect()
    {
        
    }
}

[System.Serializable]
public class RadialSection
{
    public GameObject sectionObject;
    public List<GameObject> attributes;
    public Image highlightImage;

    public void SetHighlight(bool active)
    {
        highlightImage.enabled = active; // jank
        highlightImage.gameObject.SetActive(active);
    }

    public void ShowAttribute(int index)
    {
        for (int i = 0; i < attributes.Count; i++)
        {
            attributes[i].SetActive(true); // jank

            if (i == index)
            {
                // Apply color shift when selected
                Color newColor = new Color(1.0f, 1.0f, 1.0f); // Shifts hue based on index
                attributes[i].GetComponent<Image>().color = newColor;
            }
            else 
            {
                // Apply color shift when selected
                Color newColor = new Color(0.4f, 0.4f, 0.4f); // Shifts hue based on index
                attributes[i].GetComponent<Image>().color = newColor;
            }
        }
    }
}
