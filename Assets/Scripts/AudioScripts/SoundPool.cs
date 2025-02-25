using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmp = Instantiate(objectToPool);
            
            // Ensure the object has an AudioSource
            if (!tmp.GetComponent<AudioSource>())
            {
                tmp.AddComponent<AudioSource>();
            }

            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Fetch an inactive pooled object
    public GameObject GetPooledObject()
    {
        foreach (var pooledObj in pooledObjects)
        {
            if (!pooledObj.activeInHierarchy)
            {
                return pooledObj;
            }
        }

        if (pooledObjects.Count < amountToPool)
        {
            GameObject tmp = Instantiate(objectToPool);
            pooledObjects.Add(tmp);
            return tmp;
        }
        Debug.LogWarning("No available pooled objects!");
        return null;
    }
}
