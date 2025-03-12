using UnityEngine;

public class TurnOnObjectScript : MonoBehaviour
{

    public GameObject portal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            portal.SetActive(true);
        }
    }
}
