using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PunchScript : MonoBehaviour
{
    
    public int damage = 1;

    public GameObject enemy;

    public UnityEvent<GameObject> punchTarget;
    
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
            enemy = other.gameObject;
            other.GetComponent<SuperPupSystems.Helper.Health>()?.Damage(damage);
            punchTarget.Invoke(enemy);
            Debug.Log(" Enemy Hit");
        }
        

    }
    public void Punch(GameObject enemy)
    {
        
           
    }
   
}
