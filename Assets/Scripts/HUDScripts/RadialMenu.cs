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
        // Keyboard Input for toggling the radial menu
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMenu(true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMenu(false);
        }

        // If the menu is active and 'E' is pressed, cycle the radial sections
        if (menuActive && Input.GetKeyDown(KeyCode.E))
        {
            CycleRadial();
        }

        // Debugging the menu state
        Debug.Log("Menu Active: " + menuActive);
    }

    void ToggleMenu(bool state)
    {
        Debug.Log("Toggle Menu: " + state); // Debug log to check if it's being triggered
        radialMenu.SetActive(state);
        menuActive = state;
        if (state)
        {
            HighlightCurrentStyle();
        }
    }

    void CycleRadial()
    {
        // Cycle through the attributes and styles of the radial sections
        if (currentAttributeIndex < radialSections[currentStyleIndex].attributes.Count - 1)
        {
            currentAttributeIndex++;
        }
        else
        {
            currentAttributeIndex = 0;
            currentStyleIndex = (currentStyleIndex + 1) % radialSections.Count;
        }
        HighlightCurrentStyle();
    }

    void HighlightCurrentStyle()
    {
        // Highlight the current radial style and show the corresponding attribute
        for (int i = 0; i < radialSections.Count; i++)
        {
            radialSections[i].SetHighlight(i == currentStyleIndex);
        }
        radialSections[currentStyleIndex].ShowAttribute(currentAttributeIndex);
    }

    void CycleElementUp()
    {
        // Cycle through the elements (e.g., aspects or spells)
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
        // Cycle through different aspects or states
        if (currentStyleIndex == 0)
        {
            currentStyleIndex = 1;
        }
        else
        {
            currentStyleIndex = 0;
        }
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
        // Show the currently selected attribute in the radial section
        for (int i = 0; i < attributes.Count; i++)
        {
            attributes[i].SetActive(i == index);
        }
    }
}
