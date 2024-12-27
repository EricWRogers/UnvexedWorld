using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public bool InRange = false;

     public GameObject DiologueBox;

     public Diologue text;

     

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
        if (other.gameObject.CompareTag("Player"))
        { 
            DiologueBox.SetActive(true);
            InRange = true;
            text.StartDiolague();
             
        }
    }

     void OnTriggerExit(Collider other) 
    {
        InRange = false;
        DiologueBox.SetActive(false);
         text.textComponent.text = string.Empty;
    }
    
}
