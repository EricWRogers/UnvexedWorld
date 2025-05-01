using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public string sceneName;
    public TMP_Text dialogueText;
    public TMP_Text skipText;
    public bool wasPressed;
    public bool secondPress;
    private bool gabbaGoo;

    PlayerGamepad gamepad;

    void Awake()
    {
        Time.timeScale = 20.0f;
        //gamepad = new PlayerGamepad();

        //gamepad.GamePlay.Enable();

        //gamepad.GamePlay.Jump.performed += ctx => SkipScene();
    }

    void OnDestroy()
    {
        Time.timeScale = 1.0f;
        //gamepad.GamePlay.Jump.performed -= ctx => SkipScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire"))
        {
            SkipScene();
        }
    }
    public void SetText(string text)
    {
        dialogueText.text = text;
    }

    public void LoadScene(string sceneName)
    {
        //gamepad.GamePlay.Jump.performed -= ctx => SkipScene();
        SceneManager.LoadScene(sceneName);
        Destroy(this);
    }

    public void SkipScene()
    {
        Debug.Log("Skip button was pressed");
        skipText.text = "Skip";
        wasPressed = true;

        if(wasPressed && secondPress && !gabbaGoo)
        {
            gabbaGoo = true;
            LoadScene(sceneName);
            Destroy(this);
        }

        if(wasPressed)
        {
            wasPressed = false;
            secondPress = true;
        }
    }
}
