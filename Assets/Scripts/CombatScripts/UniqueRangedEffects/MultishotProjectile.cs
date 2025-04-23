using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultishotProjectile : MonoBehaviour
{
    public List<GameObject> spears;
    public int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.SetParent(gameObject.GetComponent<AttackUpdater>().player.transform);
        AudioManager.instance.PlayRangedSound(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if (index < spears.Count)
        {
            spears[index].GetComponent<ProjectileSpell>().activate.Invoke();
            spears[index].transform.SetParent(null);
            index++;
            if (index >= spears.Count)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
