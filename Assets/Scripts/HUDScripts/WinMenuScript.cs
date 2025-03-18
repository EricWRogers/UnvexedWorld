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
    public GameObject healthBar;
    public GameObject comboInfo;

    private WinGame winGameObj;
    [SerializeField]
    private PauseMenu pauseMenu;
    private ThirdPersonMovement playerMovement;
    private MeleeRangedAttack playerAttack;
    
    void Start()
    {
        playerMovement = FindFirstObjectByType<ThirdPersonMovement>();
        playerAttack = FindFirstObjectByType<MeleeRangedAttack>();

        winSection.SetActive(false);

        winGameObj = GameObject.FindFirstObjectByType<WinGame>();
        //This is a null reference
        pauseMenu = FindFirstObjectByType<PauseMenu>();
        if (winGameObj == null)
        {
            return;
        }
        
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
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        didWin = true;
        pauseMenu.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winSection.SetActive(true);
        healthBar.SetActive(false);
        comboInfo.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        Time.timeScale = 1.0f;
        pauseMenu.enabled = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        Time.timeScale = 1.0f;
        pauseMenu.enabled = true;
        SceneManager.LoadScene("BlightsGraspMenu");
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
