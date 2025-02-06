using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject unBrokenObject;
    public GameObject brokenObject;

    public string[] tagNames; //need to make this so that the we can check what can and cannot make the 

    private void Awake()
    {
        unBrokenObject.SetActive(true);
        brokenObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            unBrokenObject.SetActive(false);
            brokenObject.SetActive(true);
            Destroy(this, 1f);
        }
    }
}
