using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableOrb : MonoBehaviour
{
    public int id = 0;
    public Texture texture;
    public Texture darkTexture;
    private CollectableTracker collectableTracker;

    public GameObject collectBlock;
    public GameObject collectBlock2;

    private CollectableUIManager collectableUIManager;

    private AudioManager audioManager;

    public void Start()
    {
        //audioManager = FindObjectOfType<AudioManager>();
    }

    //Turns off opaque orb, turns on transparent orb for animation
    public void opaqueToTransparent()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }

    public void orbSound()
    {
        //audioManager.PlayOrbSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You picked up an orb");
            collectableTracker = other.gameObject.GetComponent<CollectableTracker>();
            collectableTracker.collectedOrbs += 1;
            collectBlock.GetComponent<RawImage>().texture = texture;
            collectBlock2.GetComponent<RawImage>().texture = texture;
            gameObject.GetComponent<Animator>().Play("OrbCollectAnim");

            //PlayerPrefs.SetInt("collectedOrbs", collectableTracker.collectedOrbs);

           CollectableUIManager[] cm = Resources.FindObjectsOfTypeAll<CollectableUIManager>();

           foreach(CollectableUIManager c in cm)
           {
                c.Collect(id);
           }
        }
    }

}
