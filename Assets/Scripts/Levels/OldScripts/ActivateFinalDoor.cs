using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalDoor : MonoBehaviour
{
    public Animator finalDoor;
    public GameObject finalDoorPS;
    public GameObject fogWall;

    void Start()
    {
        finalDoor.enabled = false;
        finalDoorPS.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && !fogWall.activeInHierarchy)
        {
            finalDoor.enabled = true;
            finalDoorPS.SetActive(true);
        }
    }
}
