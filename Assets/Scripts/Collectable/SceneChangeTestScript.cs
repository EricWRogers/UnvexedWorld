using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTestScript : MonoBehaviour
{

    public string nextSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            gameObject.GetComponent<GameManager>().LoadScene(nextSceneName);
    }
}
