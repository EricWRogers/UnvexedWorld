using UnityEngine;

public class GenPopUP : MonoBehaviour
{
    public PopupText popupText;

    public string popText;

    public bool playonce = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupText = FindFirstObjectByType<PopupText>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pop(string bustitDown)
    {
         popupText.AddToQueue(popText);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             
            pop(popText);
            
        }
        if(other.gameObject.tag == "Player")
        {
            if(playonce == true)
            {
                pop(popText);
                gameObject.SetActive(false);
            }
        }
    }
}
