using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinGame : MonoBehaviour
{

    private CollectableTracker collectableTracker;

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
            collectableTracker = other.gameObject.GetComponent<CollectableTracker>();
            if(collectableTracker.collectedOrbs >= 3)
            {
                //PlayerPrefs.setInt(collectableTracker.collectedOrbs, collectableTracker.collectedOrbs);
                //collectableTracker.collectedOrbs = PlayerPrefs.GetInt("collectedOrbs");
                //This should update collectedOrbs to match the total amount collected by the player
                int prev = PlayerPrefs.GetInt("collectedOrbs", 0);
                if (collectableTracker.collectedOrbs > prev)
                {
                    PlayerPrefs.SetInt("collectedOrbs", collectableTracker.collectedOrbs);
                }
                winGame.Invoke();
            }
        }
    }
}
