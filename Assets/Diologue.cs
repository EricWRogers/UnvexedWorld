using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diologue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index = 0;

    public ThirdPersonMovement movement;

    

   

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

      void Update()
    {
        if(movement.nextLine == true)
        {
            LineSkip();   
        }
    }
   

    // Update is called once per frame
    public void LineSkip()
    {
        movement.nextLine = false;
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

    public void StartDiolague()
    {
        movement.nextLine = false;
        index = 0;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    public void NextLine()
    {
        return;
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
