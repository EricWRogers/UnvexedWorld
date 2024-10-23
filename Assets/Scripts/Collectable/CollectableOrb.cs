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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You picked up an orb");
            collectableTracker = other.gameObject.GetComponent<CollectableTracker>();
            collectableTracker.collectedOrbs += 1;
            collectBlock.SetActive(false);
            Destroy(this.gameObject);
        }
    }

}
