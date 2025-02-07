using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject unBrokenObject;
    public GameObject brokenObject;

    [SerializeField]
    private Rigidbody[] rb;

    private float power = 5f;

    public string[] tagNames; //need to make this so that the we can check what can and cannot make the 

    private void Awake()
    {
        unBrokenObject.SetActive(true);
        brokenObject.SetActive(false);
        rb = brokenObject.GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            unBrokenObject.SetActive(false);
            brokenObject.SetActive(true);

            if(rb != null)
            {
                foreach(Rigidbody rigidbodies in rb)
                {
                    float mag = rigidbodies.linearVelocity.magnitude;
                    Vector3 dir = (transform.position - unBrokenObject.transform.position).normalized;
                    rigidbodies.AddForce(dir * (power + mag), ForceMode.Impulse);
                }
            }

            Destroy(this, 1f);
        }
    }
}
