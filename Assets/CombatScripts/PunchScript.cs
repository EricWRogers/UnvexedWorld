using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PunchScript : MonoBehaviour
{
    
    public int damage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
      
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit" + other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            Debug.Log(" Enemy Hit");
        }

    }
}
