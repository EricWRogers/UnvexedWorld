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

    public ThirdPersonMovement movement;

    

   

    // Start is called before the first frame update
    void Start()
    {
        
       movement = FindFirstObjectByType<ThirdPersonMovement>();
        
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
       textComponent.text = string.Empty;
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

    public void NextLine()
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
            movement.inText = false;
        }
    }
}
