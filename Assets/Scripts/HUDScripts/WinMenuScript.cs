using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class WinMenuScript : MonoBehaviour
{
    public bool didWin = false;

    public GameObject winSection;
    public GameObject powerSystem;
    public GameObject healthBar;
    public GameObject comboInfo;

    private WinGame winGameObj;
    private PauseMenu pauseMenu;
    
    void Start()
    {
        winSection.SetActive(false);

        winGameObj = GameObject.FindFirstObjectByType<WinGame>();
        if (winGameObj == null)
        {
            return;
        }
        
        pauseMenu = FindFirstObjectByType<PauseMenu>();
        if (pauseMenu == null)
        {
            return;
        }

        if (winGameObj != null)
        {
            winGameObj.winGame.AddListener(Win);
        }
    }
    public void Win()
    {
        Debug.Log("You have WON!");
        didWin = true;
        pauseMenu.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winSection.SetActive(true);
        powerSystem.SetActive(false);
        healthBar.SetActive(false);
        comboInfo.SetActive(false);
        Time.timeScale = 0.0f;
    }

     public void Retry()
    {
        
        Time.timeScale = 1.0f;
        pauseMenu.enabled = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenu.enabled = true;
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void QuitGame()
    {
        #if(UNITY_EDITOR)
        Debug.Log("Quiting Play Mode");
        EditorApplication.ExitPlaymode();
        #else
        Debug.Log("Quitting Build");
        Application.Quit();
        #endif
    }
}
