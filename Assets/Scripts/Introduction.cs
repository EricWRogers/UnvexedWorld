using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{

    public TMP_Text dialogueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetText(string text)
    {
        dialogueText.text = text;
    }

    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
}
