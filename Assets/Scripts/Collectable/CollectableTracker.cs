using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTracker : MonoBehaviour
{
    //private CollectableOrb collectedOrbs;

    public int collectedOrbs = 0;

    public GameObject collect1Block;
    public GameObject collect2Block;
    public GameObject collect3Block;
    public GameObject collect4Block;
    public GameObject collect5Block;

    //private bool collected = true;


     
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
        
    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (collectedOrbs >= 1)
        {
            //collected = false;
            collect1Block.SetActive(false);
        }
        if (collectedOrbs >=2)
        {
            collect2Block.SetActive(false);
        }
        if (collectedOrbs >=3)
        {
            collect3Block.SetActive(false);
        }
        if (collectedOrbs >=4)
        {
            collect4Block.SetActive(false);
        }
        if (collectedOrbs >=5)
        {
            collect5Block.SetActive(false);
        }
    }
    */

}

/*

Possible way to handle a list of things for order of colection

public class TargetHandler : MonoBehaviour
{
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
}

*/
