using System.Collections;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField]
    public GameObject BrokenObject; 
    public float dragonForce = 2.0f;
    public float delay = 1.0f;

    private ThirdPersonMovement player;

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
        else if(other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<ThirdPersonMovement>();
            if(player.dashing == true)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                BrokenObject.SetActive(true);
                Destroy(gameObject, 4f);
                AudioManager.instance.PlayBreakableSound();
            }
            else if(player.isJumping == true)
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
