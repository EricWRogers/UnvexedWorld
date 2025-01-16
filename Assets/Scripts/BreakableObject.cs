using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject unBrokenObject;
    public GameObject brokenObject;

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
