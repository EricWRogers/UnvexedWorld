using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField]
    public GameObject UnBrokenObject;
    [SerializeField]
    public GameObject BrokenObject; 
    [SerializeField]
    public AudioClip BreakSound;

    private AudioSource audiosource;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Breakable")
        {
            UnBrokenObject.SetActive(false);
            BrokenObject.SetActive(true);
            audiosource = GetComponent<AudioSource>();
            audiosource.clip = BreakSound;
            audiosource.Play();
        }
    }
}
