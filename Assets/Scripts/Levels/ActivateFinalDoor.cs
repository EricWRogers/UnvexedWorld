using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalDoor : MonoBehaviour
{
    public GameObject finalDoor;
    public GameObject fogWall;

    void Start()
    {
        finalDoor.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && !fogWall.activeInHierarchy)
        {
            finalDoor.SetActive(true);
        }
    }
}
