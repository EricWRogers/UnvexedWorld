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
    
    void Start()
    {
        winSection.SetActive(false);

        if(winGameObj == null)
        {
            return;
        }

        winGameObj = GameObject.FindObjectOfType<WinGame>();
        winGameObj.winGame.AddListener(Win);
    }
    public void Win()
    {
        Debug.Log("You have WON!");
        didWin = true;
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
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
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
