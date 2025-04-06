using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject unBrokenObject;
    public GameObject brokenObject;
    public Rigidbody[] rb;
    private AudioManager audioManager;

    public float breakPower = 5f;

    public string[] tagNames; //need to make this so that the we can check what can and cannot make the 

    private void Awake()
    {
        unBrokenObject.SetActive(true);
        brokenObject.SetActive(false);
        PopulateRB();
    }

    public void PopulateRB()
    {
        rb = brokenObject.GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag(tagNames[0]) || col.gameObject.CompareTag(tagNames[1]) || col.gameObject.CompareTag(tagNames[2]))
        {
            unBrokenObject.SetActive(false);
            brokenObject.SetActive(true);
            AudioManager.instance.PlayBreakableSound();

            LanuchPieces();

            Destroy(this, 1f);
        }
    }

    public void LanuchPieces()
    {
        if(rb != null)
        {
            foreach(Rigidbody rigidbodies in rb)
            {
                float mag = rigidbodies.linearVelocity.magnitude;
                Vector3 dir = (transform.position - unBrokenObject.transform.position).normalized;
                rigidbodies.AddForce(dir * (breakPower + mag), ForceMode.Impulse);
            }
        }
    }
}
