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
    public SpellShot spellShot;
    public Aspect CurrentElement = Aspect.none;
    public List<Aspect> unlockedElements = new List<Aspect>{Aspect.none};
    public float scavengeMana = 100f;
    public float splendorMana = 100f;
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
        //gamepad.GamePlay.Scavange.performed += ctx => Scavenge();
        //gamepad.GamePlay.Sunder.performed += ctx => Sunder();
        //gamepad.GamePlay.Splendor.performed += ctx => Splendor();
        gamepad.GamePlay.Clear.performed += ctx => ClearSpell();
        gamepad.GamePlay.Casting.performed += ctx => SetCasting();
        gamepad.GamePlay.Casting.canceled += ctx => UnsetCast();

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
    }

    public void PrintCastType(CastType castType)
    {
        Debug.Log("" + castType.ToString());
    }

   

    public void CastSpell(CastType castType)
    {
        if (castType == CastType.ranged)
        {
            spellShot.ShootSpellPrefab(CurrentElement);
           
        }
        if (castType == CastType.melee)
        {
            for(int i = 0; i<spells.Length; i++)
            {
                spells[i].SetSelf(CurrentElement);
            }
            
        }
    }
    

    
    //Populating the spell list
    void Scavenge()
    {
        if ( CurrentElement == Aspect.none)
        {
            CurrentElement = Aspect.scavenge;
            
        }
    }

    void Sunder()
    {
        if ( CurrentElement == Aspect.none)
        {
            CurrentElement = Aspect.sunder;
        }
    }

    void Splendor()
    {
        if ( CurrentElement == Aspect.none)
        {
            CurrentElement = Aspect.splendor;
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

    void ClearSpell()
    {
        clear = true;
        CurrentElement = Aspect.none;
    }

    //Modifying the spell script on the fist
    public void SetFistMain(Aspect aspect)
    {
        //spells[0].SetMain(aspect);
    }

    void ClearFistSpell()
    {
        spells[0].ClearSpell();
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
    public void AddTheListenerMain(SpellCraft.Aspect aspect)
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
}
