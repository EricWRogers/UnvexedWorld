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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Breakable"))
        {
            UnBrokenObject.SetActive(false);
            BrokenObject.SetActive(true);
            audiosource = GetComponent<AudioSource>();
            audiosource.clip = BreakSound;
            audiosource.Play();
        }
    }
}
