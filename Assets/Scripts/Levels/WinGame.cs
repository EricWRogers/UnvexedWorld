using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinGame : MonoBehaviour
{

    private CollectableManager collectableManager;

    public UnityEvent winGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Reference collectible tracker, only winnible if at least 3 orbs have been collected

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        { 
            collectableManager = other.gameObject.GetComponent<CollectableManager>();
            if(collectableManager.collectedOrbs >= 3)
            {
                winGame.Invoke();
            }
        }
    }
}
