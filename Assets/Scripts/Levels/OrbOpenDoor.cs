using UnityEngine;

public class OrbOpenDoor : MonoBehaviour
{
    public bool openDoor = false;

    public bool giveMeOne = false;

    public GameObject textBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            openDoor = true;
            if(textBox != null){
                textBox.SetActive(true);
            }
            gameObject.SetActive(false);
            
            
            
        }
        
    }
}
