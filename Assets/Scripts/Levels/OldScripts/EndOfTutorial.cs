using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTutorial : MonoBehaviour
{
    public string nextScene;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //next scene
            SceneManager.LoadScene(nextScene);
        }
    }
}
