using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNextLevel : MonoBehaviour
{
    public string nextScene;
    [SerializeField] Animator transitionAnim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

       public void PlayGame(string sceneName)
    {
        
        
        SceneManager.LoadScene(sceneName);
        
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        PlayGame(nextScene);
        transitionAnim.SetTrigger("Start");
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             
            
                StartCoroutine(LoadLevel());
            
              
              
                
              
        }   


    }

  
}
