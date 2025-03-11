using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyOrbCollect : MonoBehaviour
{
    public PopupText popupText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupText = FindFirstObjectByType<PopupText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popupText.AddToQueue("Key Orb");
            keyOrbGainedScript.instance.HasKeyOrb = true;
            gameObject.SetActive(false);


        }
    }
}
