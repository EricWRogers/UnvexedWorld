using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFog : MonoBehaviour
{
    public GameObject fogArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            fogArea.SetActive(true);
        }
    }
}
