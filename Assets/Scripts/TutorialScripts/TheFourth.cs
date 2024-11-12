using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFourth : MonoBehaviour
{
    public GameObject wall;

    public SpellCraft spell;

    public bool inArea = false;

    public bool check1, check2, check3 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( spell.mainSet == true && inArea == true)
        {
            check1 = true;
        }

        if(spell.modSet == true && inArea == true)
        {
            check2 = true;
        }

        if(spell.clear == true && inArea == true)
        {
            check3 = true;
        }
       

        if( check1 == true && check2 == true && check3 == true)
        {
            TurnOff();
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
            inArea = true;
            spell.clear = false;
        }
    }

    public void TurnOff()
    {
        wall.SetActive(false);
    }
}

