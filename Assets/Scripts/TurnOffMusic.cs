using UnityEngine;

public class TurnOffMusic : MonoBehaviour
{

    public bool turnOn = false;

    // Update is called once per frame
    void Update()
    {
        if (turnOn)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
    }
}
