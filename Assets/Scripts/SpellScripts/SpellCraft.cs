using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCraft : MonoBehaviour
{
    public enum CastType
    {
        melee,
        ranged
    }
    public enum Aspect
    {
        none,
        scavenge,
        splendor,
        sunder,
    }
    public Aspect CurrentElement = Aspect.none;
    public List<Aspect> unlockedElements = new List<Aspect>{Aspect.none};
    public int[] energy ={0,100,100,100};
    public int elementIndex = 0;
    public int subAspect = 0;
    public bool casting = false;

    public bool mainSet = false;


    public bool clear = false;



   
   

    PlayerGamepad gamepad;
 
     
    public Spell[] spells;


     void Awake()
    // Start is called before the first frame update
    {
        spells = GetComponentsInChildren<Spell>();
        
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Casting.performed += ctx => SetCasting();
        gamepad.GamePlay.Casting.canceled += ctx => UnsetCast();
        gamepad.GamePlay.Cycleaspect.performed += ctx => CycleAspect();
        gamepad.GamePlay.Cycleelement.performed += ctx => CycleElementUp();

    }

    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetCasting();
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            UnsetCast();
        }
        GetComponent<Animator>().SetBool("Casting", casting);
        
        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            UnlockElement((Aspect)(elementIndex%4));
        }

        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            CycleAspect();
        }

        if(Input.mouseScrollDelta.y<0)
        {
            CycleElementDown();
        }
        else if(Input.mouseScrollDelta.y>0)
        {
            CycleElementUp();
        }
    }
    public void CycleElementUp()
    {
        if(elementIndex+1 < unlockedElements.Count)
        {
            elementIndex++;
        }
        else
        {
            elementIndex = 0;
        }
        CurrentElement = unlockedElements[elementIndex];
    }
    public void CycleElementDown()
    {
        if(elementIndex-1 < 0)
        {
            elementIndex = unlockedElements.Count-1;
        }
        else
        {
            elementIndex--;
        }
        CurrentElement = unlockedElements[elementIndex];
    }

    public void CycleAspect()
    {
        if(subAspect==0)
        {
            subAspect = 1;
        }
        else
        {
            subAspect = 0;
        }
    }

    
    //controlling the casting variable
    void SetCasting()
    {
        casting = true;
    }
  void UnsetCast()
    {
        casting = false;
    }

    public void UnlockElement(Aspect aspect)
    {
        unlockedElements.Insert(unlockedElements.Count,aspect);
    }

    //setting listeners for populating list on hit
    public void AddTheListenerMain(Aspect aspect)
    {
        //gameObject.GetComponentsInChildren<PunchScript>()[1].punchTarget.AddListener(delegate{SetMain(aspect);});
    }

    
    public void RemoveTheListener()
    {
        gameObject.GetComponentsInChildren<PunchScript>()[1].punchTarget.RemoveAllListeners();
    }

    public void AOEOnMe()
    {
        Instantiate(ParticleManager.Instance.AOE, gameObject.transform.position, transform.rotation).AddComponent<HitListener>();
    }

    public void SpendEnergy(Aspect element, int amount)
    {
        if(element!=Aspect.none)
        {
            energy[(int)element]-=amount;
            if(energy[(int)element]<0)
            {
                energy[(int)element]=0;
            } 
        }
    }

    public void ResetEnergy()
    {
        energy =  new int[] {0,100,100,100};
    } 
}
