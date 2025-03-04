using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransitionScript : MonoBehaviour
{
    public keyOrbGainedScript orbGained;
     public PopupText popupText;

     public GameObject portal;
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
        
        portal.SetActive(true);
        SceneManager.LoadScene(sceneName);
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             if(orbGained.HasKeyOrb == true)
            {
                 portal.SetActive(true);
                 popupText.AddToQueue("Portal Active");
            }
              if(orbGained.HasKeyOrb == false)
              {
                popupText.AddToQueue("Need Key Orb");
              }
        }   


    }
    
}
