using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControllerDetectionIntro : MonoBehaviour
{
    public TextMeshProUGUI skipText;

    public Introduction intro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var controllers = Input.GetJoystickNames();

        if (controllers.Length > 0 && intro.secondPress == false)
        {
            skipText.text = ("Press A");

        }
        else if (controllers.Length == 0 && intro.secondPress == false)
        {
            skipText.text = ("Space");
        }

    }
}
