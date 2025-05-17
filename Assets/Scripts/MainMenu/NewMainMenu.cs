using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class NewMainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayGame(string sceneName)
    {
        Debug.Log("Next Scene");
        GameManager.Instance.hasKeyOrb = false;
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.switches.Clear();
        GameManager.Instance.battleOn = false;
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

    public void Credits(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
