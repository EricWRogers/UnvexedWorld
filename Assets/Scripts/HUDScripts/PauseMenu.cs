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
    public GameObject moveSet; // Declare your MoveSet GameObject
    public Button resumeButton;
    public Button quitButton;

    public WinMenuScript win;

    PlayerGamepad gamepad;

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
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);
        moveSet.SetActive(false); // Ensure MoveSet is hidden at the start

        // Assign button listeners
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
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
        Debug.Log("Game Paused");
        // Show the pause menu and freeze the game
        pauseMenuUI.SetActive(true);
        moveSet.SetActive(true); // Show the MoveSet UI
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Show cursor
        healthBar.SetActive(false);
        powerSystem.SetActive(false);
        comboInfo.SetActive(false);
        Time.timeScale = 0f; // Freeze time
        isPaused = true;
        
    }

    public void Resume()
    {
        Debug.Log("Game Resumed");
        // Hide the pause menu and unfreeze the game
        pauseMenuUI.SetActive(false);
        moveSet.SetActive(false); // Hide the MoveSet UI
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
        healthBar.SetActive(true);
        powerSystem.SetActive(true);
        comboInfo.SetActive(true);
        Time.timeScale = 1f; // Unfreeze time
        isPaused = false;
    }

    public void Quit()
    {
        // Unfreeze time (important before changing scenes) and load the MainMenu scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
