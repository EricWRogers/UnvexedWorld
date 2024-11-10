using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFirst : MonoBehaviour
{
    public GameObject wall;

    public MeleeRangedAttack attack;

    public bool inArea = false;

    public bool check1, check2, check3 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(attack.isAttacking == true && inArea == true)
        {
            check1 = true;
        }
        if(attack.direction == true && inArea == true)
        {
            check2 = true;
        }
        if(attack.shoot == true && inArea == true)
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
        }
    }

    public void TurnOff()
    {
        wall.SetActive(false);
    }
}
