using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject powerSystem;
    public GameObject healthBar;
    public GameObject comboInfo;
    public GameObject mana;
    public Button resumeButton;
    public Button quitButton;

    public WinMenuScript win;

    PlayerGamepad gamepad;

    private bool isPaused = false;

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
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);

        // Assign button listeners
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(MainMenu);
    }

    void Update()
    {
        // Check if the player presses the Escape key
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
        // Show the pause menu and freeze the game
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        healthBar.SetActive(false);
        powerSystem.SetActive(false);
        comboInfo.SetActive(false);
        mana.SetActive(false);
        Time.timeScale = 0f; // Freeze time
        isPaused = true;
    }

    public void Resume()
    {
        // Hide the pause menu and unfreeze the game
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthBar.SetActive(true);
        powerSystem.SetActive(true);
        comboInfo.SetActive(true);
        mana.SetActive(true);
        Time.timeScale = 1f; // Unfreeze time
        isPaused = false;
    }

    public void MainMenu()
    {
        // Unfreeze time (important before changing scenes) and load the MainMenu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
