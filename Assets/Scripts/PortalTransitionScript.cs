using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class PortalTransitionScript : MonoBehaviour
{
    // public keyOrbGainedScript orbGained;
    public PopupText popupText;

     public bool portalOn = false;
    public GameObject portal;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      popupText = FindFirstObjectByType<PopupText>();
        
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
             if(keyOrbGainedScript.instance.HasKeyOrb == true)
            {
                 portal.SetActive(true);
                 popupText.AddToQueue("Portal Active");
                 keyOrbGainedScript.instance.HasKeyOrb = false;
                 portalOn = true;
                 
              

                 
            }
              if(keyOrbGainedScript.instance.HasKeyOrb == false)
              {
                if(portalOn == false){
                  popupText.AddToQueue("Need Key Orb");
                }
                if(portalOn == true)
                {
                  popupText.AddToQueue("Portal Active");
                }
              }
        }   


    }
    
}
