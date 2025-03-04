using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class PortalTransitionScript : MonoBehaviour
{
    public keyOrbGainedScript orbGained;
    public PopupText popupText;

     public bool portalOn = false;
    public GameObject portal;

    public GameObject cylinder1;
    public GameObject cylinder2;
    public GameObject cylinder3;
    public GameObject cylinder4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      popupText = FindFirstObjectByType<PopupText>();
      orbGained = FindFirstObjectByType<keyOrbGainedScript>();
        
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
                 orbGained.HasKeyOrb = false;
                 portalOn = true;
                 cylinder1.SetActive(true);
                cylinder2.SetActive(true);
                cylinder3.SetActive(true);
                cylinder4.SetActive(true);

                 
            }
              if(orbGained.HasKeyOrb == false)
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
