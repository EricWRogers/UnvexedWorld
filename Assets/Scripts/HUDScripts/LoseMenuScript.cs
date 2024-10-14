using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class LoseMenuScript : MonoBehaviour
{
    public bool isPlayerDead = false;
    public bool didLose = false;

    public Health playerHealth;

    public GameObject loseSection;
    public GameObject powerSystem;
    public GameObject healthBar;
    public GameObject ComboMeter;

    void Start()
    {
        loseSection.SetActive(false);
    }

     public void Lose()
    {
        didLose = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loseSection.SetActive(true);
        powerSystem.SetActive(false);
        healthBar.SetActive(false);
        ComboMeter.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
