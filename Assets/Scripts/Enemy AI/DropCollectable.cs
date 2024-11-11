using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectable : MonoBehaviour
{
    public GameObject prefab;
    
    public void OnDeath()
    {
        prefab.transform.position = transform.position;
    }
}
