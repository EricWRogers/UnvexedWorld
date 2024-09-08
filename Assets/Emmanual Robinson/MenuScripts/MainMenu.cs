using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public string MenuScene = "MenuScene"; // Set to your correct scene name

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the Inspector.");
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Quit Button is not assigned in the Inspector.");
        }
    }

    void StartGame()
    {
        Debug.Log("Starting Game...");
        SceneManager.LoadScene(MenuScene);
    }

    void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();

        // This line is just for debugging in the Unity editor.
        Debug.Log("Game is exiting");
    }
}
