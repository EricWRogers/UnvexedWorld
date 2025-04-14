using System.Collections;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField]
    public GameObject BrokenObject; 
    public float dragonForce = 2.0f;
    public float delay = 1.0f;

    private AudioManager audioManager;

    void FixedUpdate()
    {
        
        delay -= Time.fixedDeltaTime;

        if (delay > 0.0f)
            return;
        
        /*

        if (rb.linearVelocity.magnitude > dragonForce)
        {
            UnBrokenObject.SetActive(false);
            BrokenObject.SetActive(true);
            Destroy(gameObject, 4f);
            AudioManager.instance.PlayBreakableSound();
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("GroundEnemy"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            BrokenObject.SetActive(true);
            Destroy(gameObject, 4f);
            AudioManager.instance.PlayBreakableSound();
        }
        else
        {
            if(other.gameObject.GetComponent<ThirdPersonMovement>().dashing == true)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                BrokenObject.SetActive(true);
                Destroy(gameObject, 4f);
                AudioManager.instance.PlayBreakableSound();
            }
            else if(other.gameObject.GetComponent<ThirdPersonMovement>().isJumping == true)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                BrokenObject.SetActive(true);
                Destroy(gameObject, 4f);
                AudioManager.instance.PlayBreakableSound();
            }


            /*if (delay > 0.0f)
            return;

            if (other.gameObject.GetComponent<Rigidbody>())
            {
                var rb = other.gameObject.GetComponent<Rigidbody>();

                if (rb.linearVelocity.magnitude > dragonForce)
                {
                    UnBrokenObject.SetActive(false);
                    BrokenObject.SetActive(true);
                    Destroy(gameObject, 4f);
                    AudioManager.instance.PlayBreakableSound();
                }
            }*/
        }
    }

    
}
