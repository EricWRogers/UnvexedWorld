using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Show cursor
         Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
