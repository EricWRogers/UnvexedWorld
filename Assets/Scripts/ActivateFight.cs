using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFight : MonoBehaviour
{
    public GameObject enemyGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            enemyGroup.SetActive(true);
            //Activate Barrier to prevent player from leaving
            Destroy(gameObject);
        }
    }
}