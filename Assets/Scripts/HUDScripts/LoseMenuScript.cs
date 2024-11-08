using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;
using UnityEngine.EventSystems;

public class LoseMenuScript : MonoBehaviour
{
    public bool isPlayerDead = false;
    public bool didLose = false;

    public GameObject loseSection;
    public GameObject powerSystem;
    public GameObject healthBar;
    public GameObject comboInfo;

    private Health playerHealth;

    public EventSystem eventSystem;
    

    void Start()
    {
        loseSection.SetActive(false);

         eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        playerHealth.outOfHealth.AddListener(Lose);    
    }

     public void Lose()
    {
        eventSystem.enabled = false;
        didLose = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loseSection.SetActive(true);
        powerSystem.SetActive(false);
        healthBar.SetActive(false);
        comboInfo.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
