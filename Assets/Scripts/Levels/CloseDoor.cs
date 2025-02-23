using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator anim;
    public Collider col;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            col.enabled = true;
            anim.SetBool("IsOpen", false);
            Destroy(this, .3f);
        }
    }
}
