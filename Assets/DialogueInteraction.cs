using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    
    public bool InRange = false;

     public GameObject DiologueBox;

     public Diologue text;

     public bool pauseGame = false;

     public bool once = false;

     

     

    // Start is called before the first frame update
    void Start()
    {
        DiologueBox.SetActive(false);
    }
 

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& pauseGame == false)
        { 
            DiologueBox.SetActive(true);
          
            text.StartDiolague();
            Debug.Log("InText");
            
        }

        if (other.gameObject.CompareTag("Player")&& pauseGame == true && once ==false){
             DiologueBox.SetActive(true);
           
            text.StartDiolague();
            Debug.Log("InText");
            Time.timeScale = 0.0f;
            once = true;

        }

       
    }

     void OnTriggerExit(Collider other) 
    {
        if(InRange == true)
        {
         DiologueBox.SetActive(false);
         text.textComponent.text = string.Empty;
        }
        
    }
    
}
