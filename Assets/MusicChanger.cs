using UnityEngine;

public class MusicChanger : MonoBehaviour
{

    public bool dontDO = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && dontDO == false)
        {
            GameManager.Instance.battleOn = true;
            dontDO = true;
            
        }
    }
}
