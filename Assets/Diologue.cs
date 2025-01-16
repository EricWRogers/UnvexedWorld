using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diologue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

   

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        
    }
   

    // Update is called once per frame
    public void LineSkip()
    {
       
       {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
       }
    }

    public void StartDiolague()
    {
       index = 0;  
       StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
