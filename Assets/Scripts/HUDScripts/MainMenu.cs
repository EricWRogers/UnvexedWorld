using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public string gameSceneName = "MenuScene"; // Set to your correct scene name

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
        SceneManager.LoadScene(gameSceneName);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
