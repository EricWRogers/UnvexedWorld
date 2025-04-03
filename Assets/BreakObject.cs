using System.Collections;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField]
    public GameObject UnBrokenObject;
    [SerializeField]
    public GameObject BrokenObject; 
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("GroundEnemy"))
        {
            UnBrokenObject.SetActive(false);
            BrokenObject.SetActive(true);
            Destroy(gameObject, 4f);
        }
    }

    
}
