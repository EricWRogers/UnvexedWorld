using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RadialMenuManager : MonoBehaviour 
{
    public GameObject radialMenu; // Parent object of the menu
    public List<RadialSection> radialSections; // List of radial sections (styles)
    
    private int currentStyleIndex = 0;
    private int currentAttributeIndex = 0;
    private bool menuActive = false;
    
    private PlayerGamepad gamepad;

    void Awake()
    {
        // Initialize the Gamepad
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Casting.performed += ctx => ToggleMenu(true);
        gamepad.GamePlay.Casting.canceled += ctx => ToggleMenu(false);
        gamepad.GamePlay.Cycleaspect.performed += ctx => CycleAspect();
        gamepad.GamePlay.Cycleelement.performed += ctx => CycleElementUp();
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
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMenu(false);
        }

        if (menuActive && Input.GetKeyDown(KeyCode.E))
        {
            CycleRadial();
        }
    }

    void ToggleMenu(bool state)
    {
        Debug.Log("Toggle Menu: " + state);
        radialMenu.SetActive(state);
        menuActive = state;

        // Show all sections when menu is opened
        if (state)
        {
            foreach (RadialSection section in radialSections)
            {
                section.sectionObject.SetActive(true);
            }
            HighlightCurrentStyle();
        }
    }

    void CycleRadial()
    {
        // Cycle through styles
        currentStyleIndex = (currentStyleIndex + 1) % radialSections.Count;
        currentAttributeIndex = 0; // Reset attribute index when changing styles
        HighlightCurrentStyle();
    }

    void HighlightCurrentStyle()
    {
        // Highlight the correct section and apply a hue shift
        for (int i = 0; i < radialSections.Count; i++)
        {
            bool isActive = (i == currentStyleIndex);
            radialSections[i].SetHighlight(isActive);
            radialSections[i].ShowAttribute(currentAttributeIndex);

            if (isActive)
            {
                // Apply color shift when selected
                Color newColor = Color.HSVToRGB((i * 0.2f) % 1, 1, 1); // Shifts hue based on index
                radialSections[i].highlightImage.color = newColor;
            }
        }
    }

    void CycleElementUp()
    {
        if (currentAttributeIndex + 1 < radialSections[currentStyleIndex].attributes.Count)
        {
            currentAttributeIndex++;
        }
        else
        {
            currentAttributeIndex = 0;
        }
        HighlightCurrentStyle();
    }

    void CycleAspect()
    {
        currentStyleIndex = (currentStyleIndex + 1) % radialSections.Count;
        HighlightCurrentStyle();
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
        highlightImage.enabled = active;
    }

    public void ShowAttribute(int index)
    {
        for (int i = 0; i < attributes.Count; i++)
        {
            attributes[i].SetActive(i == index);
        }
    }
}
