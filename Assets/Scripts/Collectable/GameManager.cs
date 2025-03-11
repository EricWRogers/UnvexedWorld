using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using SuperPupSystems.Manager;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public int coinVal = 0;

    void Awake()
    {

        if (instance == null)
            {
                instance = this;
                GameObject.DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Scene currentScene = SceneManager.GetActiveScene ();

        SceneManager.activeSceneChanged += ChangedActiveScene;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene (String nextSceneName)
    {
        //Handle saving stuff here and handle loading scene
        coinVal = WalletManager.instance.Coin;
    }

    //Call this part for new scene being loaded
    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
        UpdateCoins();
    }

    void UpdateCoins ()
    {
        //coinVal = WalletManager.instance.Coin;
        WalletManager.instance.Earn(coinVal);

    }
}
