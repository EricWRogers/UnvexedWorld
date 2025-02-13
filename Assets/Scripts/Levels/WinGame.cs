using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WinGame : MonoBehaviour
{

    private CollectableTracker collectableTracker;

    public EventSystem eventSystem;

    public UnityEvent winGame;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    //Reference collectible tracker, only winnible if at least 3 orbs have been collected

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            eventSystem.enabled = false;
            collectableTracker = other.gameObject.GetComponent<CollectableTracker>();
            if(collectableTracker.collectedOrbs >= 1)
            {
                //This is from me attempting to save progress between scenes
                //PlayerPrefs.setInt(collectableTracker.collectedOrbs, collectableTracker.collectedOrbs);
                //collectableTracker.collectedOrbs = PlayerPrefs.GetInt("collectedOrbs");
                //This should update collectedOrbs to match the total amount collected by the player
                //int prev = PlayerPrefs.GetInt("collectedOrbs", 0);
                //if (collectableTracker.collectedOrbs > prev)
                //{
                //    PlayerPrefs.SetInt("collectedOrbs", collectableTracker.collectedOrbs);
                //}
                winGame.Invoke();
            }
        }
    }
}
