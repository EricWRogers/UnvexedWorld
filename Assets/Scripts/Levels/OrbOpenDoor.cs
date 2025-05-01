using UnityEngine;

public class OrbOpenDoor : MonoBehaviour
{
    public bool openDoor = false;

    public bool giveMeOne = false;

    public GameObject textBox;

    public GameObject theLight;

    public GameObject otherLight;

    public Material AltMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            theLight.GetComponent<MeshRenderer> ().material = AltMaterial;
            if(otherLight != null)
            {
                otherLight.GetComponent<MeshRenderer> ().material = AltMaterial;
            }
            openDoor = true;
            if(textBox != null){
                textBox.SetActive(true);
            }
            gameObject.SetActive(false);
            
            
            
        }
        
    }
}
