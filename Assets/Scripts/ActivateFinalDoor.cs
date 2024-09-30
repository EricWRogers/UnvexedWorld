using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalDoor : MonoBehaviour
{
    public Animator finalDoor;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            finalDoor.SetBool("OpenDoor", true);
        }
    }
}
