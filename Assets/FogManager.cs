using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public GameObject group;

    public void Update()
    {
        if (group.transform.childCount == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
