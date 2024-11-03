using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectable : MonoBehaviour
{
    public GameObject prefab;
    
    public void OnDeath()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
