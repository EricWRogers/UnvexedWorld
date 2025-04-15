using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class PortalTransitionScript : MonoBehaviour
{
  public PopupText popupText;

  public bool portalOn = false;
  public GameObject portal;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    popupText = FindFirstObjectByType<PopupText>();      
  }

  void Update()
  {
    if (portalOn == true)
    {
      AudioManager.instance.Play("TransportPortal");
    }

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
      if(GameManager.Instance.hasKeyOrb == true)
      {
        portal.SetActive(true);
        popupText.AddToQueue("Portal Active");
        GameManager.Instance.hasKeyOrb = false;
        portalOn = true;
      }
      if(GameManager.Instance.hasKeyOrb == false)
      {
        if(portalOn == false)
        {
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
