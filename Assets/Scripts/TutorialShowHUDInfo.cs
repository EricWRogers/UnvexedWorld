using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShowHUDInfo : MonoBehaviour
{
    private HUDManager HUD;
    private bool isHUDShowing = false;

    void Awake()
    {
        HUD = GameObject.FindObjectOfType<HUDManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        HUD.ShowHUD();
        isHUDShowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHUDShowing)
        {
            HUD.ShowHUD();
            isHUDShowing = false;
        }
    }
}
