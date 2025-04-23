using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SlideManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public bool options = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Options()
    {
        eventSystem.enabled = false;
    }

    public void OptionsOff()
    {
        eventSystem.enabled = true;
    }

}
