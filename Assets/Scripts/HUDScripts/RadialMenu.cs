using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening; // Add DOTween namespace

public class RadialMenuManager : MonoBehaviour
{
    public GameObject radialMenu; // Parent object of the menu
    public GameObject centerRadial;
    public SpellCraft spellCraft;
    public List<RadialSection> radialSections; // List of radial sections (styles)

    private int currentStyleIndex = 2;
    private bool menuActive = false;

    private PauseMenu pauseMenu;
    private LoseMenuScript loseMenu;
    private PlayerGamepad gamepad;
    private AudioManager audioManager;

    void Awake()
    {
        // Initialize the Gamepad
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Cycleelement.performed += ctx => ToggleMenu(true);
        gamepad.GamePlay.Cycleelement.canceled += ctx => ToggleMenu(false);
        //ToggleMenu(false);
    }

    void Start()
    {
        spellCraft = FindAnyObjectByType<SpellCraft>();
        audioManager = FindFirstObjectByType<AudioManager>();

        pauseMenu = FindAnyObjectByType<PauseMenu>();

        loseMenu = FindAnyObjectByType<LoseMenuScript>();
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
        if(pauseMenu.isPaused == false || loseMenu.didLose == false)
        {
            

            if(Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Radial Menu Open - Play Pop In Sound");
                HighlightCurrentStyle();
                ToggleMenu(true);
                audioManager.Play("RadialPopIn");
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                Debug.Log("Radial Menu closed - Playing Pop Out Sound");
                ToggleMenu(false);
                audioManager.Play("RadialPopOut");
            }

            if (menuActive && Input.GetKeyDown(KeyCode.E))
            {
                //CycleRadial();
            }

            if (menuActive && Input.GetKeyDown(KeyCode.T)) // Cycle Aspect with T
            {
                Debug.LogFormat("Radial Menu Switch - Play Switch Sound");
                CycleAspect();
                audioManager.Play("RadialSwitch");
            }

            

            if (menuActive && radialSections[0].sectionObject.active == false)
            {
                HighlightCurrentStyle();
                ToggleMenu(true);
            }
        }

    }

    void ToggleMenu(bool state)
    {
        Debug.Log("Toggle Menu: " + state);
        menuActive = state;

        if (state)
        {
            // Animate radial sections expanding from the center
            for (int i = 0; i < radialSections.Count; i++)
            {
                Transform section = radialSections[i].sectionObject.transform;
                // Set the section to start from the center (hidden)
                section.localScale = Vector3.zero;
                section.gameObject.SetActive(true);

                // Animate expansion using DOTween (can adjust timing and ease as needed)
                section.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
                
            }

            centerRadial.transform.localScale = Vector3.zero;
            centerRadial.SetActive(true);

            // Animate expansion using DOTween (can adjust timing and ease as needed)
            centerRadial.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack);

            HighlightCurrentStyle();
        }
        else
        {
            // Animate radial sections collapsing back into the center
            for (int i = 0; i < radialSections.Count; i++)
            {
                {

                    Transform s = radialSections[i].sectionObject.transform;
                    s.DOKill();
                    // Animate collapse using DOTween (can adjust timing and ease as needed)
                    s.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack)
                        .OnKill(() => s.gameObject.SetActive(false)); // Hide after animation
                }

                if (radialSections[i].highlightImage.gameObject.active)
                {
                    Transform s = radialSections[i].highlightImage.gameObject.transform;
                    s.DOKill();
                    // Animate collapse using DOTween (can adjust timing and ease as needed)
                    s.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack)
                        .OnKill(() => s.gameObject.SetActive(false)); // Hide after animation
                }
            }

            Transform section = centerRadial.transform;
            section.DOKill();
            // Animate collapse using DOTween (can adjust timing and ease as needed)
            section.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack)
                .OnKill(() => section.gameObject.SetActive(false)); // Hide after animation
        }
    }

    void CycleRadial()
    {
        // Cycle through styles
        currentStyleIndex = (currentStyleIndex + 1) % radialSections.Count;
        HighlightCurrentStyle();
    }

    void HighlightCurrentStyle()
    {
        for (int i = 0; i < radialSections.Count; i++)
        {
            bool isActive = (i == currentStyleIndex);
            radialSections[i].SetHighlight(isActive);
            radialSections[i].ShowAttribute((int)spellCraft.CurrentElement - 1);

            if (isActive)
            {
                Color newColor = new Color(1.0f, 1.0f, 1.0f);
                radialSections[i].highlightImage.color = newColor;
            }
            else
            {
                Color newColor = new Color(0.4f, 0.4f, 0.4f);
                radialSections[i].highlightImage.color = newColor;
            }
        }
    }

    void CycleAspect()
    {
        // Add logic for cycling aspect if needed
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
        highlightImage.gameObject.SetActive(active);

        if (active)
        {
            if (highlightImage.transform.localScale != Vector3.one)
            {
                highlightImage.transform.localScale = Vector3.zero;
                // Animate expansion using DOTween (can adjust timing and ease as needed)
                highlightImage.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }
        }
    }

    public void ShowAttribute(int index)
    {
        for (int i = 0; i < attributes.Count; i++)
        {
            attributes[i].SetActive(true);

            if (i == index)
            {
                Color newColor = new Color(1.0f, 1.0f, 1.0f);
                attributes[i].GetComponent<Image>().color = newColor;
            }
            else
            {
                Color newColor = new Color(0.4f, 0.4f, 0.4f);
                attributes[i].GetComponent<Image>().color = newColor;
            }
        }
    }
}
