using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTracker : MonoBehaviour
{

    //private CollectableManager collectableTracker;

    public int collectedOrbs = 0;
   
    //PlayerPrefs.SetInt("collectedOrbs", collectedOrbs);
 
    public List<GameObject> targetCache; 
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetCache.Contains(collision.gameObject))

            if (targetCache.IndexOf(collision.gameObject) == 0)
            {
                print("Pickup Collected");
                targetCache.RemoveAt(0);
                Destroy(collision.gameObject);
            }
            else
            {
                print("Wrong item picked up");
            }
    }
    


    // Start is called before the first frame update
    void Awake()
    {
        //This is from me attempting to save progress between scenes
        //PlayerPrefs.GetInt("collectedOrbs", collectedOrbs);
        //PlayerPrefs.SetInt("collectedOrbs", collectedOrbs);
    }

}