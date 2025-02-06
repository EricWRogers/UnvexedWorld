using UnityEngine;

public class DoubleTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb.isKinematic != false)
            {
                Debug.Log(col.gameObject.name + ", You have been hit take knock back");
            }
        }
    }
}
