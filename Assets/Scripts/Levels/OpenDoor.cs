using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    public Collider col;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Unlocks on Door");
            col.enabled = false;
            anim.SetBool("isOpen", true);
        }
    }
}
