using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalDoor : MonoBehaviour
{
    public Animator finalDoor;
    public GameObject fogWall;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && !fogWall.activeInHierarchy)
        {
            finalDoor.SetBool("OpenDoor", true);
        }
    }
}
