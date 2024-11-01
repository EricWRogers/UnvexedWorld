using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableOrb : MonoBehaviour
{

    private CollectableTracker collectableTracker;

    public GameObject collectBlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Make function to change orbs sphere and sphere(1) from opaque to transparent
    //call it when collectables are collected (as animation event at start of animation)
    public void opaqueToTransparent()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You picked up an orb");
            collectableTracker = other.gameObject.GetComponent<CollectableTracker>();
            collectableTracker.collectedOrbs += 1;
            collectBlock.SetActive(false);
            gameObject.GetComponent<Animator>().Play("OrbCollectAnim");

            //PlayerPrefs.SetInt("collectedOrbs", collectableTracker.collectedOrbs);

           
        }
    }

}
