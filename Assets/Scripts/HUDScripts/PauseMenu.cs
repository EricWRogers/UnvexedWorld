using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject healthBar;
    public GameObject comboInfo;
    public GameObject moveSet; // MoveSet GameObject
    public Button resumeButton;
    public Button quitButton;
    public Button moveSetButton; // Add MoveSet button reference
    public GameObject HealthStyle;
    public GameObject radialMenu;

    public WinMenuScript win;

    PlayerGamepad gamepad;

    private ThirdPersonMovement playerMovement;
    private MeleeRangedAttack playerAttack;

    private bool isMoveSetOpen = false; // Track MoveSet visibility
    public bool isPaused = false;

    void Awake()
    {
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Pause.performed += ctx => CheckPause();
    }

    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }

    void Start()
    {
        playerMovement = FindFirstObjectByType<ThirdPersonMovement>();
        playerAttack = FindFirstObjectByType<MeleeRangedAttack>();

        pauseMenuUI.SetActive(false);
        moveSet.SetActive(false); // Ensure MoveSet is hidden at start

        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
        moveSetButton.onClick.AddListener(ToggleMoveSet); // Add listener
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && win.didWin != true)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void CheckPause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        pauseMenuUI.SetActive(true);
        moveSet.SetActive(false); // Ensure MoveSet stays hidden on pause
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        healthBar.SetActive(false);
        comboInfo.SetActive(false);
        HealthStyle.SetActive(false);
        radialMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        pauseMenuUI.SetActive(false);
        moveSet.SetActive(false); // Hide MoveSet when resuming
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthBar.SetActive(true);
        comboInfo.SetActive(true);
        HealthStyle.SetActive(true);
        radialMenu.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BlightsGraspMenu");
    }

    // **Function to Toggle MoveSet UI**
    public void ToggleMoveSet()
    {
        isMoveSetOpen = !isMoveSetOpen;
        moveSet.SetActive(isMoveSetOpen);
    }
}
