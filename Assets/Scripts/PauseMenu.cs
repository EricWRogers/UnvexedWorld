using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject powerSystem;
    public GameObject healthBar;
    public Button resumeButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);

        // Assign button listeners
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
    }

    void Update()
    {
        // Check if the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
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

    void Pause()
    {
        // Show the pause menu and freeze the game
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        healthBar.SetActive(false);
        powerSystem.SetActive(false);
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
