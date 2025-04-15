using UnityEngine;

public class OrbOpenDoor : MonoBehaviour
{
    public bool openDoor = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            openDoor = true;
            gameObject.SetActive(false);
        }
    }
}
