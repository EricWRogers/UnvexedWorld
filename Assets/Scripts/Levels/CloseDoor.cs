using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("IsOpen", false);
            Destroy(this, .3f);
        }
    }
}
